using Banking.BankAccounts.Application.Command.Accounts.Create;
using Banking.BankAccounts.Presentation.Requests.Accounts;
using Banking.Core.Models;
using Banking.Framework;
using Microsoft.AspNetCore.Mvc;
using ApplicationController = Banking.Framework.Controller.ApplicationController;

namespace Banking.BankAccounts.Presentation.Controllers;

public class AccountController : ApplicationController
{
    //Account : create, delete, softDelete, restore, getById, getAll
    
    // [HttpPost]
    // public async Task<ActionResult<Guid>> Create(
    //     [FromServices] CreateAccountHandler handler,
    //     [FromBody] CreateAccountRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     var result = await handler.Handle(request.ToCommand(), cancellationToken);
    //
    //     if (result.IsFailure)
    //         return BadRequest(Envelope.Error(result.Error));
    //
    //     return Created("", Envelope.Ok(result.Value));
    // }
}