using Banking.Core.Processors;
using Banking.Users.Application.Commands.Login;
using Banking.Users.Application.Commands.Refresh;
using Banking.Users.Application.Commands.Register.CorporateAccount;
using Banking.Users.Application.Commands.Register.IndividualAccount;
using Banking.Users.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;
using ApplicationController = Banking.Framework.Controller.ApplicationController;

namespace Banking.Users.Presentation;

public class UsersController : ApplicationController
{
    [HttpPost("individual")]
    public async Task<IActionResult> RegisterIndividualAccount(
        [FromServices] RegisterIndividualAccountHandler handler,
        [FromForm] RegisterIndividualAccountRequest request,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDto = fileProcessor.Process(request.File);
        
        return await HandleCommand(
            request,
            r => r.ToCommand(fileDto),
            handler.Handle,
            cancellationToken);
    }
    
    [HttpPost("corporate")]
    public async Task<IActionResult> RegisterCorporateAccount(
        [FromServices] RegisterCorporateAccountHandler handler,
        [FromBody] RegisterCorporateAccountRequest request,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
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
        var result =  await HandleCommand(
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
        var result =  await HandleCommand(
            request,
            r => r.ToCommand(),
            handler.Handle,
            cancellationToken);

        return result.Result;
    }
}