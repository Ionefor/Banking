using Banking.Core.Abstractions;
using Banking.SharedKernel.Models.Errors;
using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Contracts.Response;
using Banking.UserAccounts.Domain;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banking.UserAccounts.Application.Command.Auth.Login;

public record LoginUserHandler : ICommandHandler<LoginResponse, LoginUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;

    public LoginUserHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);
      
        if (user is null)
        {
            return Errors.General.
                NotFound(
                    new ErrorParameters.General.NotFound(
                        nameof(User), nameof(command.Email), command.Email)).ToErrorList();
        }
        
        var passwordConfirmed = 
            await _userManager.CheckPasswordAsync(user, command.Password);
        
        if (!passwordConfirmed)
            return Errors.Extra.CredentialsIsInvalid().ToErrorList();
        
        var accessToken = _tokenProvider.
            GenerateAccessToken(user, cancellationToken);
        
        var refreshToken = _tokenProvider.
            GenerateRefreshToken(user, accessToken.Result.Jti, cancellationToken);
        
        return new LoginResponse(accessToken.Result.AccessToken, refreshToken.Result);
    }
}