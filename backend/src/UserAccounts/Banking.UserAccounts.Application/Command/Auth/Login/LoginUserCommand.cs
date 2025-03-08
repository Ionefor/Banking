using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Command.Auth.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;