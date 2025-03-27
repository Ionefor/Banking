using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.CompanName;

public record UpdateCompanyNameCommand(Guid AccountId, string CompanyName) : ICommand;
