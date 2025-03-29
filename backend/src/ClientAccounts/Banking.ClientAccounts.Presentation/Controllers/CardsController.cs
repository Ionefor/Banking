using Banking.BankAccounts.Application.Features.Command.Cards.Add;
using Banking.BankAccounts.Application.Features.Command.Cards.Delete;
using Banking.BankAccounts.Application.Features.Command.Cards.SetMainCard;
using Banking.BankAccounts.Application.Features.Queries.Cards.GetByAccountId;
using Banking.BankAccounts.Application.Features.Queries.Cards.GetByClientAccountId;
using Banking.BankAccounts.Application.Features.Queries.Cards.GetById;
using Banking.ClientAccounts.Presentation.Requests.Card;
using Banking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationController = Banking.Framework.Controller.ApplicationController;

namespace Banking.ClientAccounts.Presentation.Controllers;

public class CardsController : ApplicationController
{
    [Permission(Permissions.Cards.Create)]
    [HttpPost("/ClientAccounts/{clientAccountId:guid}/card")]
    public async Task<ActionResult<Guid>> AddCard(
        [FromRoute] Guid clientAccountId,
        [FromBody] AddCardRequest request,
        [FromServices] AddCardHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleCommand(
            request,
            r => r.ToCommand(clientAccountId),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [Permission(Permissions.Cards.Delete)]
    [HttpDelete("/ClientAccounts/{clientAccountId:guid}/cards/{cardId:guid}")]
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
    
    [Permission(Permissions.Cards.Update)]
    [HttpPut("/ClientAccounts/{clientAccountId:guid}/cards/{cardId:guid}/main-card")]
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
    
    [Permission(Permissions.Cards.Read)]
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
    
    [Permission(Permissions.Cards.Read)]
    [HttpGet("/bankAccounts/{bankAccountId:guid}/cards")]
    public async Task<ActionResult<Guid>> GetByBankAccountId(
        [FromRoute] Guid bankAccountId,
        [FromQuery] GetCardsWithPaginationRequest request,
        [FromServices] GetByAccountIdHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleQuery(
            request,
            r => r.ToAccountQuery(bankAccountId), 
            handler.Handle, 
            cancellationToken
        );
    }
    
    [Permission(Permissions.Cards.Read)]
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