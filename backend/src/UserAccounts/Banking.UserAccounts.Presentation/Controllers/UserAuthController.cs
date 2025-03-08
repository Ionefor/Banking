using Banking.Framework;
using Banking.UserAccounts.Application.Commands.Auth.Login;
using Banking.UserAccounts.Application.Commands.Auth.Refresh;
using Banking.UserAccounts.Application.Commands.Auth.Register;
using Banking.UserAccounts.Presentation.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Banking.UserAccounts.Presentation.Controllers;

public class UserAuthController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromServices] RegisterUserHandler handler,
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        return await HandleRequest(
            request,
            r => r.ToCommand(),
            handler.Handle,
            cancellationToken);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result =  await HandleRequest(
            request,
            r => r.ToCommand(),
            handler.Handle,
            cancellationToken);

        return result.Result;
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenHandler handler,
        CancellationToken cancellationToken)
    {
        var result =  await HandleRequest(
            request,
            r => r.ToCommand(),
            handler.Handle,
            cancellationToken);

        return result.Result;
    }
}