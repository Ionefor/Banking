using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Account.Update.Tax;

public record UpdateTaxIdCommand(Guid UserId, string TaxId) : ICommand;