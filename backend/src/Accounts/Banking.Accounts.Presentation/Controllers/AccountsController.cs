using Banking.Accounts.Application.Commands.Delete;
using Banking.Accounts.Application.Commands.Update.Address;
using Banking.Accounts.Application.Commands.Update.Mail;
using Banking.Accounts.Application.Commands.Update.Number;
using Banking.Accounts.Application.Queries.GetAll;
using Banking.Accounts.Application.Queries.GetByUserId;
using Banking.Accounts.Presentation.Requests.General;
using Banking.Core.Models;
using Banking.Framework.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [Permission(Permissions.Accounts.Delete)]
    [HttpDelete("{accountId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid accountId,
        [FromServices] DeleteUserAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            accountId,
            id => new DeleteUserAccountCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [Permission(Permissions.Accounts.Read)]
    [HttpGet]
    public async Task<ActionResult> GetAll(
        [FromQuery] GetAccountsWithPaginationRequest request,
        [FromServices] GetAccountsWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleQuery(
            request,
            r => r.ToQuery(), 
            handler.Handle, 
            cancellationToken
        );
    }

    [Permission(Permissions.Accounts.Read)]
    [HttpGet("{accountId:guid}")]
    public async Task<ActionResult<Guid>> GetByUserId(
        [FromServices] GetAccountsByUserIdHandler handler,
        [FromRoute] Guid accountId,
        CancellationToken cancellationToken)
    {
        var result =  await HandleQuery(
            accountId,
            id => new GetAccountByUserIdQuery(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
        
        if (result.Value.Item1 is not null)
        {
            return Ok(result.Value.Item1);
        }

        var a = result.Value.Item2;
        return Ok(result.Value.Item2);
    }
    
    [Permission(Permissions.Accounts.Update)]
    [HttpPut("{accountId:guid}/phone-number")]
    public async Task<ActionResult<Guid>> UpdatePhoneNumber(
        [FromRoute] Guid accountId,
        [FromBody] UpdatePhoneNumberRequest request,
        [FromServices] UpdatePhoneNumberHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            accountId,
            request,
            (r, id) => r.ToCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [Permission(Permissions.Accounts.Update)]
    [HttpPut("{accountId:guid}/email")]
    public async Task<ActionResult<Guid>> UpdateEmail(
        [FromRoute] Guid accountId,
        [FromBody] UpdateEmailRequest request,
        [FromServices] UpdateEmailHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            accountId,
            request,
            (r, id) => r.ToCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [Permission(Permissions.Accounts.Update)]
    [HttpPut("{accountId:guid}/address")]
    public async Task<ActionResult<Guid>> UpdateAddress(
        [FromRoute] Guid accountId,
        [FromBody] UpdateAddressRequest request,
        [FromServices] UpdateAddressHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            accountId,
            request,
            (r, id) => r.ToCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
}