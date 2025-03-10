using Banking.Core.Abstractions;

namespace Banking.Accounts.Application.Commands.Update.CompanName;

public record UpdateCompanyNameCommand(Guid UserId, string CompanyName) : ICommand;
