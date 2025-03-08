using Banking.UserAccounts.Application.Command.Auth.Refresh;

namespace Banking.UserAccounts.Presentation.Requests.Auth;

public record RefreshTokenRequest(string AccessToken, Guid RefreshToken)
{
    public RefreshTokenCommand ToCommand() => new(AccessToken, RefreshToken);
}