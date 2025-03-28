﻿using Banking.Accounts.Contracts.Dto.Queries;
using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Queries.Cards.GetByClientAccountId;

public record GetByClientAccountIdQuery(
    Guid ClientAccountId, PaginationParamsDto PaginationParams) : IQuery;