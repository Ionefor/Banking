using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Auth.Refresh;

public record RefreshTokenCommand(string AccessToken, Guid RefreshToken) : ICommand;