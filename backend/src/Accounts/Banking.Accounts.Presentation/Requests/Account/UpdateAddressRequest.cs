using Banking.Accounts.Application.Commands.Update.Address;
using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Accounts.Presentation.Requests.Account;

public record UpdateAddressRequest(AddressDto Address)
{
    public UpdateAddressCommand ToCommand(Guid accountId) => new(accountId, Address);

}