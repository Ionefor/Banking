using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.Cards.SetMainCard;

public record SetMainCardCommand(Guid ClientAccountId, Guid CardId) : ICommand;