using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.ValueObjects.Ids;

public class AccountId(Guid id) : BaseId<AccountId>(id)
{
    public static implicit operator Guid(AccountId accountId) => accountId.Id;
    public static implicit operator AccountId(Guid id) => new(id);
}