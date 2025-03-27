using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Accounts.Application.Commands.Update.Photos;

public class UpdatePhotoCommandValidator : AbstractValidator<UpdatePhotoCommand>
{
    public UpdatePhotoCommandValidator()
    {
        RuleFor(u => u.AccountId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.File.FileName).
            MustBeValueObject(FilePath.Create);
    }
}