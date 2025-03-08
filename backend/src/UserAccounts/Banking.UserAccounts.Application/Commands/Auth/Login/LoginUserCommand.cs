using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Auth.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;