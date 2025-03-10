using Banking.SharedKernel.Models.Errors;
using Banking.Users.Application.Abstractions;
using Banking.Users.Domain;
using Banking.Users.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.Users.Infrastructure.Authorization;

public class RefreshSessionManager(UsersWriteDbContext userAccountsWriteDbContext) :
    IRefreshSessionManager
{
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken)
    {
        var token =  await userAccountsWriteDbContext.RefreshSessions.
            Include(s => s.User).
            FirstOrDefaultAsync(r => r.RefreshToken == refreshToken);

        if (token is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound(
                    nameof(RefreshSession), nameof(refreshToken), refreshToken));
        }

        return token;
    }
    
    public void Delete(RefreshSession refreshSession)
    {
        userAccountsWriteDbContext.Remove(refreshSession);
    }
}