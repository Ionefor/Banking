using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.Name;

public record UpdateFullNameCommand(Guid AccountId, FullNameDto FullName) : ICommand;
