using Banking.Core.Abstractions;
using Banking.UserAccounts.Contracts.Dto.Commands;

namespace Banking.UserAccounts.Application.Commands.Account.Update.AddressUser;

public record UpdateAddressCommand(Guid UserId, AddressDto Address) : ICommand;
