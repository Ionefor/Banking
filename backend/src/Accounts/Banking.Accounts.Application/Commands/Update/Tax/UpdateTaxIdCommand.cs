using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.Tax;

public record UpdateTaxIdCommand(
    Guid AccountId, string TaxId) : ICommand;