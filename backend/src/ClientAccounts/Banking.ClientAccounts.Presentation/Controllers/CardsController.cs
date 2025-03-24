using Banking.BankAccounts.Application.Command.Cards.Add;
using Banking.BankAccounts.Application.Command.Cards.Delete;
using Banking.BankAccounts.Application.Command.Cards.SetMainCard;
using Banking.BankAccounts.Application.Queries.Cards.GetByAccountId;
using Banking.BankAccounts.Application.Queries.Cards.GetByClientAccountId;
using Banking.BankAccounts.Application.Queries.Cards.GetById;
using Banking.ClientAccounts.Presentation.Requests.Card;
using Banking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationController = Banking.Framework.Controller.ApplicationController;

namespace Banking.ClientAccounts.Presentation.Controllers;

public class CardsController : ApplicationController
{
    [HttpPost("/clientAccounts/{clientAccountId:guid}/account/{accountId:guid}/cards")]
    public async Task<ActionResult<Guid>> AddCard(
        [FromRoute] Guid clientAccountId,
        [FromRoute] Guid accountId,
        [FromBody] AddCardRequest request,
        [FromServices] AddCardHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            request,
            r => r.ToCommand(clientAccountId, accountId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpDelete("/{clientAccountId:guid}/cards/{cardId:guid}")]
    public async Task<ActionResult<Guid>> DeleteCard(
        [FromRoute] Guid clientAccountId,
        [FromRoute] Guid cardId,
        [FromServices] DeleteCardHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            cardId,
            (cId, aId) => new DeleteCardCommand(clientAccountId, cardId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpPut("/{clientAccountId:guid}/cards/{cardId:guid}/main-card")]
    public async Task<ActionResult<Guid>> SetMainCard(
        [FromRoute] Guid clientAccountId,
        [FromRoute] Guid cardId,
        [FromServices] SetMainCardHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            clientAccountId,
            cardId,
            (cId, aId) => new SetMainCardCommand(clientAccountId, cardId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpGet("/cards/{cardId:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromRoute] Guid cardId,
        [FromServices] GetCardByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await HandleQuery(
            cardId,
            c => new GetCardByIdQuery(cardId), 
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    
        return result.Result;
    }
    
    [HttpGet("accounts/{accountId:guid}/cards")]
    public async Task<ActionResult<Guid>> GetByAccountId(
        [FromRoute] Guid accountId,
        [FromQuery] GetCardsWithPaginationRequest request,
        [FromServices] GetByAccountIdHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleQuery(
            request,
            r => r.ToAccountQuery(accountId), 
            handler.Handle, 
            cancellationToken
        );
    }
    
    [HttpGet("/{clientAccountId:guid}/cards")]
    public async Task<ActionResult<Guid>> GetByClientAccountId(
        [FromRoute] Guid clientAccountId,
        [FromQuery] GetCardsWithPaginationRequest request,
        [FromServices] GetByClientAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleQuery(
            request,
            r => r.ToQuery(clientAccountId), 
            handler.Handle, 
            cancellationToken
        );
    }
}