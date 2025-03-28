﻿using Banking.Accounts.Domain;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.Accounts.Application.Abstractions;

public interface IAccountRepository
{
    Task Add<TAccount>(TAccount account, CancellationToken cancellationToken) where TAccount : class;

    Task Delete<TAccount>(TAccount account) where TAccount : class;
    
    Task<Result<IndividualAccount, Error>> GetIndividualByUserId(
        Guid userId, CancellationToken cancellationToken = default);
    
    Task<Result<CorporateAccount, Error>> GetCorporateByUserId(
        Guid userId, CancellationToken cancellationToken = default);
}