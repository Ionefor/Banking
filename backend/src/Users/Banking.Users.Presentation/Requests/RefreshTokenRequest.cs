using Banking.Users.Application.Commands.Refresh;

namespace Banking.Users.Presentation.Requests;

public record RefreshTokenRequest(string AccessToken, Guid RefreshToken)
{
    public RefreshTokenCommand ToCommand() =>
        new(AccessToken, RefreshToken);
}