using Banking.SharedKernel.Models.Abstractions;

namespace Banking.BankAccounts.Domain.ValueObjects.Ids;

public class CardId(Guid id) : BaseId<CardId>(id)
{
    public static implicit operator Guid(CardId cardId) => cardId.Id;
    public static implicit operator CardId(Guid id) => new(id);
}