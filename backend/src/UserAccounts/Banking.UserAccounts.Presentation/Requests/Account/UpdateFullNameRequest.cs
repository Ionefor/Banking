using Banking.UserAccounts.Application.Commands.Account.Update.Name;
using Banking.UserAccounts.Contracts.Dto;
using Banking.UserAccounts.Contracts.Dto.Commands;

namespace Banking.UserAccounts.Presentation.Requests.Account;

public record UpdateFullNameRequest(FullNameDto FullName)
{
    public UpdateFullNameCommand ToCommand(Guid userId) => new(userId, FullName);
}