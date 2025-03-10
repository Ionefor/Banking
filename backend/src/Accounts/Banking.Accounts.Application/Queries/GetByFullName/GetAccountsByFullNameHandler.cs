using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Contracts.Dto.Models;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.Accounts.Application.Queries.GetByFullName;


public class GetAccountsByFullNameHandler : 
    IQueryHandler<Result<PageList<IndividualAccountDto>, ErrorList>, GetAccountsByFullNameQuery>

{
    private readonly IReadDbContext _readDbContext;

    public GetAccountsByFullNameHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PageList<IndividualAccountDto>, ErrorList>> Handle(
        GetAccountsByFullNameQuery query,
        CancellationToken cancellationToken = default)
    {
        var individualAccounts = _readDbContext.IndividualAccounts.
            Where(i => i.FullName == query.FullName);

        if (!individualAccounts.Any())
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(
                    nameof(IndividualAccountDto), nameof(query.FullName),
                    query.FullName)).ToErrorList();
        }
        
        #region Sorting
        
            if (query.SortingParams is not null)
            {
                individualAccounts.SortIf(
                    query.SortingParams.DateOfBirth is true,
                    q => q.DateOfBirth);
                
                individualAccounts.SortIf(
                    query.SortingParams.PhoneNumber is true,
                    q => q.PhoneNumber);
                
                individualAccounts.SortIf(
                    query.SortingParams.Address is true,
                    q => q.Address);
            }
            
        #endregion
        
        return await individualAccounts.ToPagedList(
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }
}