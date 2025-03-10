using Banking.Accounts.Application.Commands.Delete;
using Banking.Accounts.Application.Commands.Update.Address;
using Banking.Accounts.Application.Commands.Update.CompanName;
using Banking.Accounts.Application.Commands.Update.Mail;
using Banking.Accounts.Application.Commands.Update.Name;
using Banking.Accounts.Application.Commands.Update.Number;
using Banking.Accounts.Application.Commands.Update.Photos;
using Banking.Accounts.Application.Commands.Update.Tax;
using Banking.Accounts.Application.Queries.GetAll;
using Banking.Accounts.Application.Queries.GetByUserId;
using Banking.Accounts.Presentation.Requests.Account;
using Banking.Accounts.Presentation.Requests.Profile;
using Banking.Core.Models;
using Banking.Core.Processors;
using Banking.Framework.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid userId,
        [FromServices] DeleteUserAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleRequest(
            userId,
            id => new DeleteUserAccountCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpGet("/accounts")]
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

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<Guid>> GetByUserId(
        [FromServices] GetAccountsByUserIdHandler handler,
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var result =  await HandleQuery(
            userId,
            id => new GetAccountByUserIdQuery(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
        
        if (result.Value.Item1 is not null)
        {
            return Ok(result.Value.Item1);
        }
        
        return Ok(result.Value.Item2);
    }
    
    [HttpPut("{userId:guid}/phone-number")]
    public async Task<ActionResult<Guid>> UpdatePhoneNumber(
        [FromRoute] Guid userId,
        [FromBody] UpdatePhoneNumberRequest request,
        [FromServices] UpdatePhoneNumberHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleRequest(
            userId,
            request,
            (r, id) => r.ToCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpPut("{userId:guid}/email")]
    public async Task<ActionResult<Guid>> UpdateEmail(
        [FromRoute] Guid userId,
        [FromBody] UpdateEmailRequest request,
        [FromServices] UpdateEmailHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleRequest(
            userId,
            request,
            (r, id) => r.ToCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpPut("{userId:guid}/address")]
    public async Task<ActionResult<Guid>> UpdateAddress(
        [FromRoute] Guid userId,
        [FromBody] UpdateAddressRequest request,
        [FromServices] UpdateAddressHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleRequest(
            userId,
            request,
            (r, id) => r.ToCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
}