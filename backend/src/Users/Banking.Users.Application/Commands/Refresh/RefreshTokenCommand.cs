using Banking.Core.Abstractions;

namespace Banking.Users.Application.Commands.Refresh;

public record RefreshTokenCommand(string AccessToken, Guid RefreshToken) : ICommand;