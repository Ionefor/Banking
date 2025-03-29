using Banking.Accounts.Application.Commands.Update.Name;
using Banking.Accounts.Application.Commands.Update.Photos;
using Banking.Accounts.Application.Queries.GetByFullName;
using Banking.Accounts.Presentation.Requests.Individual;
using Banking.Core.Models;
using Banking.Core.Processors;
using Banking.Framework.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Presentation.Controllers;

public class IndividualAccountsController : ApplicationController
{
    [Permission(Permissions.IndividualAccounts.Read)]
    [HttpGet("full-name")]
    public async Task<ActionResult<Guid>> GetByFullName(
        [FromQuery] GetAccountsByFullNameRequest request,
        [FromServices] GetAccountsByFullNameHandler handler,
        CancellationToken cancellationToken)
    {
        var result =  await HandleQuery(
            request,
            r => r.ToQuery(), 
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);

        return result.Result;
    }
    
    [Permission(Permissions.IndividualAccounts.Update)]
    [HttpPut("{accountId:guid}/full-name")]
    public async Task<ActionResult<Guid>> UpdateFullName(
        [FromRoute] Guid accountId,
        [FromBody] UpdateFullNameRequest request,
        [FromServices] UpdateFullNameHandler handler,
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
    
    [Permission(Permissions.IndividualAccounts.Update)]
    [HttpPut("{accountId:guid}/photo")]
    public async Task<ActionResult<Guid>> UpdatePhoto(
        [FromRoute] Guid accountId,
        [FromForm] UpdatePhotoRequest request,
        [FromServices] UpdatePhotoHandler handler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDto = fileProcessor.Process(request.File);
        
        return await HandleCommand(
            accountId,
            request,
            (r, id) => r.ToCommand(id, fileDto),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
}