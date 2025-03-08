using Banking.Core.Models;
using Banking.Framework;
using Banking.UserAccounts.Application.Command.Account.Delete;
using Microsoft.AspNetCore.Mvc;

namespace Banking.UserAccounts.Presentation.Controllers;

public class AccountController : ApplicationController
{
    //Account : delete, softDelete, restore
   
    [HttpDelete("{id:guid}/user")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid userId,
        [FromServices] DeleteUserAccountHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserAccountCommand(userId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(Envelope.Ok(result.Value));
    }
}