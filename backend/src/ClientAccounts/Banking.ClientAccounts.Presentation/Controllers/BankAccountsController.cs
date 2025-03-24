using Banking.BankAccounts.Application.Command.Accounts.Add;
using Banking.BankAccounts.Application.Command.Accounts.Delete;
using Banking.BankAccounts.Application.Queries.Accounts.GetByClientAccountId;
using Banking.BankAccounts.Application.Queries.Accounts.GetById;
using Banking.ClientAccounts.Presentation.Requests.Accounts;
using Banking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationController = Banking.Framework.Controller.ApplicationController;

namespace Banking.ClientAccounts.Presentation.Controllers;

public class BankAccountsController : ApplicationController
{
    [HttpPost("/ClientAccounts/{clientAccountId:guid}/")]
    public async Task<ActionResult<Guid>> AddAccount(
        [FromRoute] Guid clientAccountId,
        [FromBody] AddAccountRequest request,
        [FromServices] AddAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            request,
            r => r.ToCommand(clientAccountId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpDelete("/{clientAccountId:guid}/accounts/{accountId:guid}")]
    public async Task<ActionResult<Guid>> DeleteAccount(
        [FromRoute] Guid clientAccountId,
        [FromRoute] Guid accountId,
        [FromServices] DeleteAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            accountId,
            (cId, aId) => new DeleteAccountCommand(clientAccountId, accountId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpGet("/{clientAccountId:guid}/accounts")]
    public async Task<ActionResult> GetByClientAccountId(
        [FromQuery] GetAccountsWithPaginationRequest request,
        [FromRoute] Guid clientAccountId,
        [FromServices] GetAccountsWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleQuery(
            request,
            r => r.ToQuery(clientAccountId), 
            handler.Handle,
            cancellationToken
        );
    }
    
    [HttpGet("/{clientAccountId:guid}/accounts/{accountId:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromRoute] Guid clientAccountId,
        [FromRoute] Guid accountId,
        [FromServices] GetAccountByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await HandleQuery(
            clientAccountId,
            accountId,
            (c, a) => new GetAccountByIdQuery(clientAccountId, accountId), 
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    
        return result.Result;
    }
}