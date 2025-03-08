using Banking.Core.Abstractions;
using Banking.UserAccounts.Contracts.Dto;
using Banking.UserAccounts.Contracts.Dto.Commands;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Name;

public record UpdateFullNameCommand(Guid UserId, FullNameDto FullName) : ICommand;
