using Banking.Core.Validation;
using Banking.UserAccounts.Domain.ValueObjects;
using FluentValidation;

namespace Banking.UserAccounts.Application.Commands.Auth.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(l => l.Email).
            MustBeValueObject(Email.Create);
        
        RuleFor(l => l.Password).
            NotNull().NotEmpty().
            WithMessage("Password must be provided.");
    }
}