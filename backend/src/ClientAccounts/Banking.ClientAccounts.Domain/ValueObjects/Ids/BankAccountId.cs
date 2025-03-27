using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.ValueObjects.Ids;

public class BankAccountId(Guid id) : BaseId<BankAccountId>(id)
{
    public static implicit operator Guid(BankAccountId bankAccountId) => bankAccountId.Id;
    public static implicit operator BankAccountId(Guid id) => new(id);
}