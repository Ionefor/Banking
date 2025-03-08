using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Command.Auth.Refresh;

public record RefreshTokenCommand(string AccessToken, Guid RefreshToken) : ICommand;