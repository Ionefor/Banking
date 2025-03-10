using Banking.Accounts.Contracts.Dto.Commands;
using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.Address;

public record UpdateAddressCommand(Guid UserId, AddressDto Address) : ICommand;
