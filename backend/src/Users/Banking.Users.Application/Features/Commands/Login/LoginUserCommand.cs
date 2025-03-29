using Banking.Core.Abstractions;

namespace Banking.Users.Application.Features.Commands.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;