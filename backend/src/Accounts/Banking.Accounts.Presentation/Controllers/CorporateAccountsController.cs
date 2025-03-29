using Banking.Accounts.Application.Commands.Update.CompanName;
using Banking.Accounts.Application.Commands.Update.Tax;
using Banking.Accounts.Application.Queries.GetByCompanyName;
using Banking.Accounts.Presentation.Requests.Corporate;
using Banking.Core.Models;
using Banking.Framework.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Presentation.Controllers;

public class CorporateAccountsController : ApplicationController
{
    [Permission(Permissions.CorporateAccounts.Read)]
    [HttpGet("company-name")]
    public async Task<ActionResult<Guid>> GetByCompanyName(
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
    
    [Permission(Permissions.CorporateAccounts.Update)]
    [HttpPut("{accountId:guid}/company-name")]
    public async Task<ActionResult<Guid>> UpdateCompanyName(
        [FromRoute] Guid accountId,
        [FromBody] UpdateCompanyNameRequest request,
        [FromServices] UpdateCompanyNameHandler handler,
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
    
    [Permission(Permissions.CorporateAccounts.Update)]
    [HttpPut("{accountId:guid}/taxId")]
    public async Task<ActionResult<Guid>> UpdateTaxId(
        [FromRoute] Guid accountId,
        [FromBody] UpdateTaxIdRequest request,
        [FromServices] UpdateTaxIdHandler handler,
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
}