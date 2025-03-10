using Banking.Core.Validation;
using Banking.SharedKernel.ValueObjects;
using FluentValidation;

namespace Banking.Users.Application.Commands.Login;

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