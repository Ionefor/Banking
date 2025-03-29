using Banking.BankAccounts.Application.Abstractions;
using Banking.BankAccounts.Contracts.Dto;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.BankAccounts.Application.Features.Queries.BankAccounts.GetById;

public class GetAccountByIdHandler(IReadDbContext readDbContext) :
    IQueryHandler<Result<BankAccountDto, ErrorList>, GetAccountByIdQuery>
{
    public async Task<Result<BankAccountDto, ErrorList>> Handle(
        GetAccountByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var clientAccount = await readDbContext.ClientAccounts.
            FirstOrDefaultAsync(c => c.Id == query.ClientAccountId, cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(query.ClientAccountId),
                    query.ClientAccountId)).ToErrorList();
        }
        
        var account = await readDbContext.Accounts.FirstOrDefaultAsync(
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