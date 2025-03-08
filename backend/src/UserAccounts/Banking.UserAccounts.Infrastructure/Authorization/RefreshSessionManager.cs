using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.UserAccounts.Infrastructure.Authorization;

public class RefreshSessionManager(UserAccountsWriteDbContext userAccountsWriteDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken)
    {
        var token =  await userAccountsWriteDbContext.RefreshSessions.
            Include(s => s.User).
            FirstOrDefaultAsync(r => r.RefreshToken == refreshToken);

        if (token is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(RefreshSession), nameof(refreshToken), refreshToken));
        }

        return token;
    }
    
    public void Delete(RefreshSession refreshSession)
    {
        userAccountsWriteDbContext.Remove(refreshSession);
    }
}