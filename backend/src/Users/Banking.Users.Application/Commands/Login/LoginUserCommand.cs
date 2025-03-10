using Banking.Core.Abstractions;

namespace Banking.Users.Application.Commands.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;