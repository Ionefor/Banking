using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Account.Delete;

public record DeleteUserAccountCommand(Guid UserId) : ICommand;