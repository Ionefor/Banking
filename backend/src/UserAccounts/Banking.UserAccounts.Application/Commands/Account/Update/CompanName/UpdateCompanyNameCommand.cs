using Banking.Core.Abstractions;

namespace Banking.UserAccounts.Application.Commands.Account.Update.CompanName;

public record UpdateCompanyNameCommand(Guid UserId, string CompanyName) : ICommand;
