using Banking.UserAccounts.Application.Commands.Account.Update.AddressUser;
using Banking.UserAccounts.Contracts.Dto;
using Banking.UserAccounts.Contracts.Dto.Commands;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdateAddressRequest(AddressDto Address)
{
    public UpdateAddressCommand ToCommand(Guid userId) => new(userId, Address);

}