using Banking.Users.Application.Features.Commands.Login;

namespace Banking.Users.Presentation.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public LoginUserCommand ToCommand() =>
        new(Email, Password);
}