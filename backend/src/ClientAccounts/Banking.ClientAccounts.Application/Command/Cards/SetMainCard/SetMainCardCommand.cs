using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Command.Cards.SetMainCard;

public record SetMainCardCommand(Guid ClientAccountId, Guid CardId) : ICommand;