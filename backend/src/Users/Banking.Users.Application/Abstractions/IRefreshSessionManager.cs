using Banking.SharedKernel.Models.Errors;
using Banking.Users.Domain;
using CSharpFunctionalExtensions;

namespace Banking.Users.Application.Abstractions;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken);
    
    void Delete(RefreshSession refreshSession);
}