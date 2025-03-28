﻿using Banking.Accounts.Application.Commands.Update.CompanName;

namespace Banking.Accounts.Presentation.Requests.Account;

public record UpdateCompanyNameRequest(string CompanyName)
{
    public UpdateCompanyNameCommand ToCommand(Guid userId) => new(userId, CompanyName);
}