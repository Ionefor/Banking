using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Delete;

public record DeleteUserAccountCommand(Guid UserId) : ICommand;