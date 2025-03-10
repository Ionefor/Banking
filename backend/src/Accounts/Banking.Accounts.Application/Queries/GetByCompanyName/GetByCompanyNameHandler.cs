using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Contracts.Dto.Models;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.Accounts.Application.Queries.GetByCompanyName;

public class GetByCompanyNameHandler : 
    IQueryHandler<Result<CorporateAccountDto, ErrorList>, GetByCompanyNameQuery>

{
    private readonly IReadDbContext _readDbContext;

    public GetByCompanyNameHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<CorporateAccountDto, ErrorList>> Handle(
        GetByCompanyNameQuery query, CancellationToken cancellationToken = default)
    {
        var corporateAccount = await _readDbContext.CorporateAccounts.
            FirstOrDefaultAsync(a => a.CompanyName == query.CompanyName, cancellationToken);

        if (corporateAccount is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(
                    nameof(CorporateAccountDto),
                    nameof(query.CompanyName),
                    query.CompanyName)).ToErrorList();
        }

        return corporateAccount;
    }
}