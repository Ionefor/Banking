using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Application.PhotoProvider;
using Banking.Accounts.Contracts;
using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Accounts.Domain;
using Banking.Core.Abstractions;
using Banking.Core.Dto;
using Banking.Core.Messaging;
using Banking.SharedKernel.Definitions;
using Banking.SharedKernel.Models.Errors;
using Banking.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;
    private readonly IAccountRepository _accountRepository;
    private readonly IPhotoProvider _photoProvider;
    private readonly IMessageQueue<PhotoMetaData> _messageQueue;
    
    public AccountsContract(
        [FromKeyedServices(ModulesName.Accounts)]IUnitOfWork unitOfWork,
        IReadDbContext readDbContext, 
        IAccountRepository accountRepository,
        IPhotoProvider photoProvider,
        IMessageQueue<PhotoMetaData> messageQueue)
    {
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
        _photoProvider = photoProvider;
        _messageQueue = messageQueue;
    }
    
    public async Task<UnitResult<ErrorList>> CreateIndividualAccount(
        Guid userId, FullNameDto fullName, AddressDto address, CreateFileDto file, string phoneNumber,
        DateOnly birthDate, string email, CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.
            BeginTransaction(cancellationToken);

        try
        {
            var name = FullName.Create(
                fullName.FirstName,
                fullName.MiddleName,
                fullName.LastName).Value;
        
            var currentAddress = Address.Create(
                address.Country,
                address.City,
                address.Street,
                address.HouseNumber).Value;
            
            var mail = Email.Create(email).Value;

            var existPhone = _readDbContext.IndividualAccounts.
                Any(i => i.PhoneNumber == phoneNumber);

            if (existPhone)
            {
                return Errors.Extra.AlreadyExists(
                    new ErrorParameters.ValueAlreadyExists(
                        $"Individual account with phoneNumber {phoneNumber} already exists"))
                    .ToErrorList();
            }
        
            var phone = PhoneNumber.Create(phoneNumber).Value; 
            
            var filePath = FilePath.Create(file.FileName).Value;
            
            var fileData = 
                new PhotoData(file.Content, filePath, Constants.Shared.BucketNamePhotos);
            
            var uploadResult = await _photoProvider.
                UploadFile(fileData, cancellationToken);
            
            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(
                    new PhotoMetaData(fileData.BucketName, fileData.FilePath), cancellationToken);

                return uploadResult.Error.ToErrorList();
            }
            
            var dateOfBirth = DateOfBirth.Create(birthDate).Value;
        
            var individualAccount = new IndividualAccount(
                userId, name, currentAddress, mail, phone, dateOfBirth, fileData.FilePath);

            await _accountRepository.Add(individualAccount, cancellationToken);
        
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        
            transaction.Commit();
            
            return UnitResult.Success<ErrorList>();
        }
        catch (Exception e)
        {
            transaction.Rollback();

            return Errors.General.
                Failed(new ErrorParameters.Failed(
                    "Can not create individual account")).ToErrorList();
        }
    }

    public async Task<UnitResult<ErrorList>> CreateCorporateAccount(
        Guid userId, AddressDto address, string companyName, string taxId, string contactEmail,
        string contactPhone, CancellationToken cancellationToken = default)
    {
        var currentAddress = Address.Create(
            address.Country,
            address.City,
            address.Street,
            address.HouseNumber).Value;
            
        var mail = Email.Create(contactEmail).Value;
        
        var existName = _readDbContext.CorporateAccounts.
            Any(i => i.CompanyName == companyName);

        if (existName)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(
                    $"Corporate account with companyName {companyName} already exists")).ToErrorList();
        }
        
        var nameOfCompany = Name.Create(companyName).Value;
        
        var existTaxId = _readDbContext.CorporateAccounts.
            Any(i => i.TaxId == taxId);

        if (existTaxId)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(
                    $"Corporate account with TaxId {taxId} already exists")).ToErrorList();
        }
        
        var tax = TaxId.Create(taxId).Value;
        
        var existPhone = _readDbContext.CorporateAccounts.
            Any(i => i.ContactPhone == contactPhone);

        if (existPhone)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.ValueAlreadyExists(
                    $"Corporate account with ContactPhone {contactPhone} already exists")).ToErrorList();
        }
        
        var phone = PhoneNumber.Create(contactPhone).Value;
        
        var corporateAccount = new CorporateAccount(
            userId, nameOfCompany, currentAddress, tax, mail, phone);

        await _accountRepository.Add(corporateAccount, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return UnitResult.Success<ErrorList>();
    }
}