using Banking.Framework;
using Banking.Framework.Extensions;
using Banking.UserAccounts.Application.Command.Auth.Login;
using Banking.UserAccounts.Application.Command.Auth.Refresh;
using Banking.UserAccounts.Application.Command.Auth.Register;
using Banking.UserAccounts.Presentation.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Banking.UserAccounts.Presentation.Controllers;

public class AuthController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<ActionResult<Guid>> Register(
        [FromServices] RegisterUserHandler handler,
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.
            Handle(request.ToCommand(), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.
            Handle(request.ToCommand(), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}