﻿using Banking.Accounts.Contracts.Dto.Queries;
using Banking.Core.Abstractions;

namespace Banking.BankAccounts.Application.Queries.Cards.GetByAccountId;

public record GetByAccountIdQuery(
    Guid AccountId, PaginationParamsDto PaginationParams) : IQuery;