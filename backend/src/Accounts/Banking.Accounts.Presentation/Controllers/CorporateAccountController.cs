using Banking.Accounts.Application.Commands.Update.CompanName;
using Banking.Accounts.Application.Commands.Update.Tax;
using Banking.Accounts.Application.Queries.GetByCompanyName;
using Banking.Accounts.Presentation.Requests.Account;
using Banking.Accounts.Presentation.Requests.Profile;
using Banking.Core.Models;
using Banking.Framework.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Presentation.Controllers;

public class CorporateAccountController : ApplicationController
{
    [HttpGet("company-name")]
    public async Task<ActionResult<Guid>> GetUserByCompanyName(
        [FromQuery] GetByCompanyNameRequest request,
        [FromServices] GetByCompanyNameHandler handler,
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
    
    [HttpPut("{userId:guid}/company-name")]
    public async Task<ActionResult<Guid>> UpdateCompanyName(
        [FromRoute] Guid userId,
        [FromBody] UpdateCompanyNameRequest request,
        [FromServices] UpdateCompanyNameHandler handler,
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
    
    [HttpPut("{userId:guid}/taxId")]
    public async Task<ActionResult<Guid>> UpdateTaxId(
        [FromRoute] Guid userId,
        [FromBody] UpdateTaxIdRequest request,
        [FromServices] UpdateTaxIdHandler handler,
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