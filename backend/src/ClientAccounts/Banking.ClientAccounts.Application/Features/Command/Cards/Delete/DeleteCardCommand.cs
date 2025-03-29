using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Features.Command.Cards.Delete;

public record DeleteCardCommand(Guid ClientAccountId, Guid CardId) : ICommand;