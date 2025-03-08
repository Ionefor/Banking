using System.Security.Claims;
using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Application.Models;
using Banking.UserAccounts.Domain;
using CSharpFunctionalExtensions;

namespace Banking.UserAccounts.Application.Abstractions;

public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken);
    
    Task<Guid> GenerateRefreshToken(User user, Guid accesTokenJti, CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken cancellationToken);
}