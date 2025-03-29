using Banking.Core.Abstractions;
using Banking.Core.Response;
using Banking.SharedKernel.Models.Errors;
using Banking.Users.Application.Abstractions;
using Banking.Users.Domain;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Banking.Users.Application.Features.Commands.Login;

public class LoginUserHandler :
    ICommandHandler<LoginResponse, LoginUserCommand>
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
                NotFound(new ErrorParameters.NotFound(nameof(User),
                    nameof(command.Email), command.Email)).ToErrorList();
        }
        
        var passwordConfirmed = await _userManager.
            CheckPasswordAsync(user, command.Password);

        if (!passwordConfirmed)
        {
            return Errors.Extra.
                CredentialsIsInvalid().ToErrorList();
        }
        
        var accessToken = _tokenProvider.
            GenerateAccessToken(user, cancellationToken);
        
        var refreshToken = _tokenProvider.
            GenerateRefreshToken(user, accessToken.Result.Jti, cancellationToken);
        
        return new LoginResponse(accessToken.Result.AccessToken, refreshToken.Result);
    }
}