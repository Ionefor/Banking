using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Command.Account.Delete;

public record DeleteUserAccountCommand(Guid UserId) : ICommand;