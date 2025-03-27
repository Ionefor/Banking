using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Contracts.Dto.Models;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.Accounts.Application.Queries.GetByUserId;


public class GetAccountsByUserIdHandler : 
    IQueryHandler<Result<(IndividualAccountDto?, CorporateAccountDto?), ErrorList>, GetAccountByUserIdQuery>

{
    private readonly IReadDbContext _readDbContext;

    public GetAccountsByUserIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }


    public async Task<Result<(IndividualAccountDto?, CorporateAccountDto?), ErrorList>> Handle(
        GetAccountByUserIdQuery query, CancellationToken cancellationToken = default)
    {
        var individualAccount = await _readDbContext.IndividualAccounts.
            FirstOrDefaultAsync(i => i.Id == query.AccountId, cancellationToken);

        var corporateAccount = await _readDbContext.CorporateAccounts.
            FirstOrDefaultAsync(c => c.Id == query.AccountId, cancellationToken);

        if (individualAccount is null && corporateAccount is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(
                    nameof(Accounts), nameof(query.AccountId), query.AccountId)).ToErrorList();
        }

        return (individualAccount, corporateAccount);
    }
}