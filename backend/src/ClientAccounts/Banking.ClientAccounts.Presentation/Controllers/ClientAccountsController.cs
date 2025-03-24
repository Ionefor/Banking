using Banking.BankAccounts.Application.Command.ClientAccounts.Create;
using Banking.BankAccounts.Application.Command.ClientAccounts.Delete;
using Banking.BankAccounts.Application.Command.ClientAccounts.Restore;
using Banking.BankAccounts.Application.Command.ClientAccounts.SoftDelete;
using Banking.BankAccounts.Application.Queries.ClientAccounts.GetAll;
using Banking.BankAccounts.Application.Queries.ClientAccounts.GetById;
using Banking.ClientAccounts.Presentation.Requests.ClientAccounts;
using Banking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationController = Banking.Framework.Controller.ApplicationController;

namespace Banking.ClientAccounts.Presentation.Controllers;

public class ClientAccountsController : ApplicationController
{
    [HttpPost("{userAccountId:guid}")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid userAccountId,
        [FromServices] CreateClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateClientAccountCommand(userAccountId);
        
        var result = await handler.Handle(command, cancellationToken);
    
        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));
    
        return Created("", Envelope.Ok(result.Value));
    }
    
    [HttpDelete("{clientAccountId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid clientAccountId,
        [FromServices] DeleteClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClientAccountCommand(clientAccountId);
        
        var result = await handler.Handle(command, cancellationToken);
    
        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));
    
        return Ok(result.Value);
    }
    
    [HttpDelete("{clientAccountId:guid}")]
    public async Task<ActionResult<Guid>> SoftDelete(
        [FromRoute] Guid clientAccountId,
        [FromServices] SoftDeleteCAHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new SoftDeleteCACommand(clientAccountId);
        
        var result = await handler.Handle(command, cancellationToken);
    
        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));
    
        return Ok(result.Value);
    }
    
    [HttpPost("{clientAccountId:guid}")]
    public async Task<ActionResult<Guid>> Restore(
        [FromRoute] Guid clientAccountId,
        [FromServices] RestoreClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RestoreClientAccountCommand(clientAccountId);
        
        var result = await handler.Handle(command, cancellationToken);
    
        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));
    
        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll(
        [FromQuery] GetCAWithPaginationRequest request,
        [FromServices] GetCAWithPaginationHandler handler,
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
        [FromServices] GetClientAccountByIdHandler handler,
        [FromRoute] Guid accountId,
        CancellationToken cancellationToken)
    {
        var result = await HandleQuery(
            accountId,
            id => new GetClientAccountByIdQuery(id), 
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);

        return result.Result;
    }
}