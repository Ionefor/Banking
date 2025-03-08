using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Photos;

public class UpdatePhotoCommandValidator : AbstractValidator<UpdatePhotoCommand>
{
    public UpdatePhotoCommandValidator()
    {
        RuleFor(u => u.UserId).
            NotEmpty().NotNull().WithMessage("UserId cannot be empty");

        RuleFor(u => u.FilePath).
            MustBeValueObject(FilePath.Create);
    }
}