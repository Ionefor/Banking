using Banking.UserAccounts.Application.Command.Auth.Login;

namespace Banking.UserAccounts.Presentation.Requests.Auth;

public record LoginUserRequest(string Email, string Password)
{
    public LoginUserCommand ToCommand() =>
        new(Email, Password);
}