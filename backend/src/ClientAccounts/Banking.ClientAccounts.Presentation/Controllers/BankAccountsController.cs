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
    [Permission(Permissions.BankAccounts.Create)]
    [HttpPost("/ClientAccounts/{clientAccountId:guid}/bankAccount")]
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
    
    [Permission(Permissions.BankAccounts.Delete)]
    [HttpDelete("/{clientAccountId:guid}/bankAccounts/{bankAccountId:guid}")]
    public async Task<ActionResult<Guid>> DeleteAccount(
        [FromRoute] Guid clientAccountId,
        [FromRoute] Guid bankAccountId,
        [FromServices] DeleteAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            bankAccountId,
            (cId, aId) => new DeleteAccountCommand(clientAccountId, bankAccountId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [Permission(Permissions.BankAccounts.Read)]
    [HttpGet("/{clientAccountId:guid}/bankAccounts")]
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
    
    [Permission(Permissions.BankAccounts.Read)]
    [HttpGet("/{clientAccountId:guid}/bankAccounts/{accountId:guid}")]
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