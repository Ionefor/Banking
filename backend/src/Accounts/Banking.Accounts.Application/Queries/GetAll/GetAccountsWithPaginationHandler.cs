using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Contracts.Dto.Models;
using Banking.Core.Abstractions;
using Banking.Core.Extension;
using Banking.Core.Models;
using Banking.SharedKernel;

namespace Banking.Accounts.Application.Queries.GetAll;

public class GetAccountsWithPaginationHandler : 
    IQueryHandler<PageList<IndividualAccountDto, CorporateAccountDto>,
        GetAccountsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetAccountsWithPaginationHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PageList<IndividualAccountDto, CorporateAccountDto>> Handle
        (GetAccountsWithPaginationQuery query, CancellationToken cancellationToken = default)
    {
        var individualAccounts = _readDbContext.IndividualAccounts;
        var corporateAccounts = _readDbContext.CorporateAccounts;
        
        if (query.FilteringParams is not null)
        {
            if (query.FilteringParams.AccountType == AccountType.Individual)
            {
                individualAccounts = SortFilterIndividualQuery(individualAccounts, query);
            }
            else if (query.FilteringParams.AccountType == AccountType.Corporate)
            {
                corporateAccounts = SortFilterCorporateQuery(corporateAccounts, query);
            }
            else
            {
                individualAccounts = SortFilterIndividualQuery(individualAccounts, query);
                corporateAccounts = SortFilterCorporateQuery(corporateAccounts, query);
            }
        }
        
        return await individualAccounts.ToPagedList(corporateAccounts,
            query.PaginationParams.Page,
            query.PaginationParams.PageSize,
            cancellationToken);
    }

    private IQueryable<IndividualAccountDto> SortFilterIndividualQuery(
        IQueryable<IndividualAccountDto> individualAccounts,
        GetAccountsWithPaginationQuery query)
    {
        individualAccounts.WhereIf(
            query.FilteringParams!.AccountId != null,
            q => q.Id == query.FilteringParams.AccountId);
                    
        individualAccounts.WhereIf(
            query.FilteringParams!.Address != null,
            q => q.Address == query.FilteringParams.Address);
                    
        individualAccounts.WhereIf(
            query.FilteringParams!.Email != null,
            q => q.Email == query.FilteringParams.Email);
                    
        individualAccounts.WhereIf(
            query.FilteringParams!.Phone != null,
            q => q.PhoneNumber == query.FilteringParams.Phone);
        
        if (query.SortingParams is not null)
        {
            individualAccounts.SortIf(
                query.SortingParams.AccountsId is true,
                q => q.Id);
        
            individualAccounts.SortIf(
                query.SortingParams.Address is true,
                q => q.Address);
        }
        
        return individualAccounts;
    }
    
    private IQueryable<CorporateAccountDto> SortFilterCorporateQuery(
        IQueryable<CorporateAccountDto> corporateAccountDto,
        GetAccountsWithPaginationQuery query)
    {
        corporateAccountDto.WhereIf(
            query.FilteringParams!.AccountId != null,
            q => q.Id == query.FilteringParams.AccountId);
                    
        corporateAccountDto.WhereIf(
            query.FilteringParams!.Address != null,
            q => q.Address == query.FilteringParams.Address);
                    
        corporateAccountDto.WhereIf(
            query.FilteringParams!.Email != null,
            q => q.ContactEmail == query.FilteringParams.Email);
                    
        corporateAccountDto.WhereIf(
            query.FilteringParams!.Phone != null,
            q => q.ContactPhone == query.FilteringParams.Phone);
        
        if (query.SortingParams is not null)
        {
            corporateAccountDto.SortIf(
                query.SortingParams.AccountsId is true,
                q => q.Id);
        
            corporateAccountDto.SortIf(
                query.SortingParams.Address is true,
                q => q.Address);
        }
        
        return corporateAccountDto;
    }
}
    