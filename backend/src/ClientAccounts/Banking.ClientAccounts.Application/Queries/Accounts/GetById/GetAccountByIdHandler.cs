using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.BankAccounts.Application.Queries.Accounts.GetById;

public class GetAccountByIdHandler  : 
    IQueryHandler<Result<AccountDto, ErrorList>, GetAccountByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetAccountByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<AccountDto, ErrorList>> Handle(
        GetAccountByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var clientAccount = await _readDbContext.ClientAccounts.
            FirstOrDefaultAsync(c => c.Id == query.ClientAccountId, cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(query.ClientAccountId),
                    query.ClientAccountId)).ToErrorList();
        }
        
        var account = await _readDbContext.Accounts.FirstOrDefaultAsync(
                c => c.Id == query.AccountId && c.ClientAccountId == query.ClientAccountId,
                cancellationToken);

        if (account is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(Accounts), nameof(query.AccountId),
                    query.AccountId)).ToErrorList();
        }

        return account;
    }
}