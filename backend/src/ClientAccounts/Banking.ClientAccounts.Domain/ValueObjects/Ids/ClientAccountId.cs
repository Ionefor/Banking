using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.ValueObjects.Ids;

public class ClientAccountId(Guid id) : BaseId<ClientAccountId>(id)
{
    public static implicit operator Guid(ClientAccountId clientAccountId) => clientAccountId.Id;
    public static implicit operator ClientAccountId(Guid id) => new(id);
}