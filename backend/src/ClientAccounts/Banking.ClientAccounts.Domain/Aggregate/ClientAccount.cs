using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel;
using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.Aggregate;

public class ClientAccount : SoftDeletableEntity<ClientAccountId>
{
    private readonly List<BankAccount> _accounts = [];
    private readonly List<Card> _cards = [];

    private ClientAccount(ClientAccountId id) : base(id) {}
    
    public ClientAccount(
        ClientAccountId id,
        Guid userAccountId,
        AccountType userAccountType)
        : base(id)
    {
        UserAccountId = userAccountId;
        UserAccountType = userAccountType;
    }
    public Guid UserAccountId { get; private set; }
    public AccountType UserAccountType { get; private set; }
    public IReadOnlyList<BankAccount> Accounts => _accounts;
    public IReadOnlyList<Card> Cards => _cards;

    public override void Delete()
    {
        base.Delete();

        foreach (var card in _cards)
            card.Delete();
        
        foreach (var account in _accounts)
            account.Delete();
    }
    public override void Restore()
    {
        base.Restore();

        foreach (var card in _cards)
            card.Restore();
        
        foreach (var account in _accounts)
            account.Restore();
    }

    public void AddAccount(BankAccount account)
    {
        _accounts.Add(account);
    }
    
    public void DeleteAccount(BankAccount account)
    {
        _accounts.Remove(account);
    }
    
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
    
    public void DeleteCard(Card card)
    {
        _cards.Remove(card);
    }
    
    public void SetMainCard(Card card)
    {
        var currentMainCard = _cards.FirstOrDefault(c => c.IsMain);

        currentMainCard?.ResetMainCard();

        var newMainCard = _cards.
            FirstOrDefault(c => c.Id == card.Id);
        
        newMainCard!.SetMainCard();
    }
}
// 1) Сделать Transaction Transfer
// 2) Сделать новый контроллер для Transaction Transfer
// 3) Добавить тесты
// 4) Решить проблему с Permissions построить новую систему??

