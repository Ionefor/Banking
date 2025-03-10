using Banking.Accounts.Application.Commands.Update.Name;
using Banking.Accounts.Application.Commands.Update.Photos;
using Banking.Accounts.Application.Queries.GetByFullName;
using Banking.Accounts.Presentation.Requests.Account;
using Banking.Accounts.Presentation.Requests.Profile;
using Banking.Core.Models;
using Banking.Core.Processors;
using Banking.Framework.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Presentation.Controllers;

public class IndividualAccountsController : ApplicationController
{
    [HttpGet("full-name")]
    public async Task<ActionResult<Guid>> GetUserByFullName(
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
    
    [HttpPut("{userId:guid}/full-name")]
    public async Task<ActionResult<Guid>> UpdateFullName(
        [FromRoute] Guid userId,
        [FromBody] UpdateFullNameRequest request,
        [FromServices] UpdateFullNameHandler handler,
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
    
    [HttpPut("{userId:guid}/photo")]
    public async Task<ActionResult<Guid>> UpdatePhoto(
        [FromRoute] Guid userId,
        [FromForm] UpdatePhotoRequest request,
        [FromServices] UpdatePhotoHandler handler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDto = fileProcessor.Process(request.File);
        
        return await HandleRequest(
            userId,
            request,
            (r, id) => r.ToCommand(id, fileDto),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
}