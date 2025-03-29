using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Banking.Core.Models;
using Banking.SharedKernel.Models.Errors;
using Banking.Users.Application.Abstractions;
using Banking.Users.Application.Models;
using Banking.Users.Domain;
using Banking.Users.Infrastructure.Authorization;
using Banking.Users.Infrastructure.DbContexts;
using Banking.Users.Infrastructure.Options;
using Banking.Users.Infrastructure.Seeding;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Banking.Users.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly UsersWriteDbContext _userAccountsWriteDbContext;
    private readonly PermissionManager _permissionManager;

    public JwtTokenProvider(
        IOptions<JwtOptions> options,
        UsersWriteDbContext userAccountsWriteDbContext,
        PermissionManager permissionManager)
    {
        _jwtOptions = options.Value;
        _userAccountsWriteDbContext = userAccountsWriteDbContext;
        _permissionManager = permissionManager;
    }
    
   public async Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.Key));
        
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles
            .Select(r => new Claim(CustomClaims.Role, r.Name ?? string.Empty));
        
        var permissions = await _permissionManager.
            GetUserPermissions(user.Id, cancellationToken);
        
        var permissionClaims = permissions.
            Select(p => new Claim(CustomClaims.Permission, p));
        
        var jti = Guid.NewGuid();

        Claim[] claims =
        [
            new(CustomClaims.Id, user.Id.ToString()),
            new(CustomClaims.Email, user.Email ?? ""),
            new(CustomClaims.Jti, jti.ToString())
        ];

        claims = claims
            .Concat(roleClaims)
            .Concat(permissionClaims)
            .ToArray();
        
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpiredMinutesTime)),
            signingCredentials: signingCredentials,
            claims: claims);
        
        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtTokenResult(stringToken, jti);
    }

    public async Task<Guid> GenerateRefreshToken(User user,
        Guid accessTokenJti,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = new RefreshSession
        {
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(60),
            Jti = accessTokenJti,
            RefreshToken = Guid.NewGuid()
        };

        _userAccountsWriteDbContext.RefreshSessions.Add(refreshSession);
        
        await _userAccountsWriteDbContext.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken;
    }

    public async Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(
        string jwtToken, 
        CancellationToken cancellationToken = default)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var validationParameters = TokenValidationParametersFactory.
            CreateWithoutLifeTime(_jwtOptions);

        var validationResult =  await jwtHandler.
            ValidateTokenAsync(jwtToken, validationParameters);

        if (!validationResult.IsValid)
            return Errors.Extra.TokenIsInvalid();

        return validationResult.ClaimsIdentity.Claims.ToList();
    }
}