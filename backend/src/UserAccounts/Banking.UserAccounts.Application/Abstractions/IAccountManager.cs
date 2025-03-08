using Banking.UserAccounts.Domain.Accounts;

namespace Banking.UserAccounts.Application.Abstractions;

public interface IAccountManager
{
    public Task CreateAdminAccount(AdminAccount adminAccount);

    public Task CreateIndividualAccount(IndividualAccount individualAccount);
    
    public Task CreateCorporateAccount(CorporateAccount corporateAccount);

    public Task<(IndividualAccount?, CorporateAccount?)> GetAccountByUserId(Guid userId);
    
    public Task DeleteIndividualAccount(IndividualAccount individualAccount);
    
    public Task DeleteCorporateAccount(CorporateAccount corporateAccount);
    
    public Task<bool> AdminAccountExists(CancellationToken cancellationToken = default);
}