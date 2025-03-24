using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel;
using Banking.SharedKernel.Models.Abstractions;

namespace Banking.ClientAccounts.Domain.Aggregate;

public class ClientAccount : SoftDeletableEntity<ClientAccountId>
{
    private readonly List<Account> _accounts = [];
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
    public IReadOnlyList<Account> Accounts => _accounts;
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

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }
    
    public void DeleteAccount(Account account)
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
        var currentCard = _cards.
            FirstOrDefault(c => c.Id == card.Id);
        
        currentCard!.SetMainCard();
    }
    
    
    // Account: create, update(wallets, cards), delete, softDelete, restore
    // Wallet : add, update(only balance), delete, softDelete, restore
    // Card : add, delete
    // Cards <- Account -> Wallets
    // Card -> Wallet
}

// 1) Доделать Account && Card Controllers - V
// 2) Добавить Permissions
// 3) Проверить методы GetUsersAccounts, Account && Card && ClientAccounts Controllers
// 4) Рефакторинг всего проекта
// 5) Добавить тесты
