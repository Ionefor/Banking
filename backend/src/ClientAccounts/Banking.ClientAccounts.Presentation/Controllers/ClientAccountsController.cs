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
    [HttpPost("Accounts/{userAccountId:guid}")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid userAccountId,
        [FromServices] CreateClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            userAccountId,
            cId => new CreateClientAccountCommand(cId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpDelete("{clientAccountId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid clientAccountId,
        [FromServices] DeleteClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            cId => new DeleteClientAccountCommand(cId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpDelete("{clientAccountId:guid}/soft")]
    public async Task<ActionResult<Guid>> SoftDelete(
        [FromRoute] Guid clientAccountId,
        [FromServices] SoftDeleteCAHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            cId => new SoftDeleteCACommand(cId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpPost("{clientAccountId:guid}/restore")]
    public async Task<ActionResult<Guid>> Restore(
        [FromRoute] Guid clientAccountId,
        [FromServices] RestoreClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            cId => new RestoreClientAccountCommand(cId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
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
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await HandleQuery(
            userId,
            id => new GetClientAccountByIdQuery(id), 
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    
        return result.Result;
    }
}