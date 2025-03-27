using Banking.Accounts.Application.Commands.Update.Name;
using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Accounts.Presentation.Requests.Account;

public record UpdateFullNameRequest(FullNameDto FullName)
{
    public UpdateFullNameCommand ToCommand(Guid accountId) => new(accountId, FullName);
}