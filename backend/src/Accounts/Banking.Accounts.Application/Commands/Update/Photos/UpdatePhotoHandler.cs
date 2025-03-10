using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Application.PhotoProvider;
using Banking.Accounts.Domain;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Messaging;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.Accounts.Application.Commands.Update.Photos;

public class UpdatePhotoHandler : ICommandHandler<Guid, UpdatePhotoCommand>
{
    private readonly ILogger<UpdatePhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<UpdatePhotoCommand> _validator;
    private readonly IPhotoProvider _photoProvider;
    private readonly IMessageQueue<PhotoMetaData> _messageQueue;

    public UpdatePhotoHandler(
        IValidator<UpdatePhotoCommand> validator,
        ILogger<UpdatePhotoHandler> logger,
        [FromKeyedServices(ModulesName.Accounts)]IUnitOfWork unitOfWork,
        IReadDbContext readDbContext,
        IAccountRepository accountRepository,
        IPhotoProvider photoProvider,
        IMessageQueue<PhotoMetaData> messageQueue)
    {
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
        _photoProvider = photoProvider;
        _messageQueue = messageQueue;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePhotoCommand command,
        CancellationToken cancellationToken = default)
    { 
        var transaction = await _unitOfWork.
            BeginTransaction(cancellationToken);

        try
        {
            var validationResult = await _validator.
                ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                return validationResult.ToErrorList();
        
            var individualExist = await _readDbContext.IndividualAccounts.
                AnyAsync(i => i.UserId == command.UserId, cancellationToken);
            
            if (!individualExist)
            {
                return Errors.General.
                    NotFound(new ErrorParameters.NotFound
                        (nameof(IndividualAccount), nameof(command.UserId), command.UserId)).
                    ToErrorList();
            }
            
            var individualAccount = _accountRepository.
                GetIndividualByUserId(command.UserId, cancellationToken).Result.Value;
            
            var filePath = FilePath.Create(command.File.FileName).Value;
        
            var fileData = 
                new PhotoData(command.File.Content, filePath, Constants.Shared.BucketNamePhotos);
            
            var uploadResult = await _photoProvider.
                UploadFile(fileData, cancellationToken);
            
            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(
                    new PhotoMetaData(fileData.BucketName, fileData.FilePath), cancellationToken);

                return uploadResult.Error.ToErrorList();
            }
            
            var photoToDeleteMeta = new PhotoMetaData(
                Constants.Shared.BucketNamePhotos, individualAccount.Photo);
            
            var deleteResult = await _photoProvider.
                DeleteFile(photoToDeleteMeta, cancellationToken);
            
            if (deleteResult.IsFailure)
                return deleteResult.Error.ToErrorList();
            
            individualAccount.UpdatePhoto(fileData.FilePath);
        
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        
            _logger.
                LogInformation("User with ID {UserId} update photo.", command.UserId);

            transaction.Commit();
            
            return command.UserId;
        }
        catch (Exception e)
        {
            transaction.Rollback();

            return Errors.General.
                Failed(new ErrorParameters.Failed("Can not update photo")).ToErrorList();
        }
    }
}