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
            FirstOrDefaultAsync(a => a.UserId == query.UserId, cancellationToken);

        var corporateAccount = await _readDbContext.CorporateAccounts.
            FirstOrDefaultAsync(a => a.UserId == query.UserId, cancellationToken);

        if (individualAccount is null && corporateAccount is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(
                    "User", nameof(query.UserId), query.UserId)).ToErrorList();
        }

        return (individualAccount, corporateAccount);
    }
}