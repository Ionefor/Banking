using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Domain;
using CSharpFunctionalExtensions;

namespace Banking.UserAccounts.Application.Abstractions;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken);
    
    void Delete(RefreshSession refreshSession);
}