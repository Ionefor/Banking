using Banking.Core.Models;
using Banking.Framework;
using Banking.UserAccounts.Application.Commands.Account.Delete;
using Banking.UserAccounts.Application.Commands.Account.Update.AddressUser;
using Banking.UserAccounts.Application.Commands.Account.Update.CompanName;
using Banking.UserAccounts.Application.Commands.Account.Update.mail;
using Banking.UserAccounts.Application.Commands.Account.Update.Name;
using Banking.UserAccounts.Application.Commands.Account.Update.Number;
using Banking.UserAccounts.Application.Commands.Account.Update.Photos;
using Banking.UserAccounts.Application.Commands.Account.Update.Tax;
using Banking.UserAccounts.Presentation.Requests.Account;
using Microsoft.AspNetCore.Mvc;

namespace Banking.UserAccounts.Presentation.Controllers;

public class UserAccountsController : ApplicationController
{
    [HttpDelete("{userId:guid}/userAccounts")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid userId,
        [FromServices] DeleteUserAccountHandler handler,
        CancellationToken cancellationToken)
    {
        return await HandleRequest(
            userId,
            id => new DeleteUserAccountCommand(id),
            handler.Handle,
            error => BadRequest(Envelope.Error(error)),
            cancellationToken);
    }
    
    [HttpPut("{userId:guid}/fullName")]
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
    
    [HttpPut("{userId:guid}/phoneNumber")]
    public async Task<ActionResult<Guid>> UpdatePhoneNumber(
        [FromRoute] Guid userId,
        [FromBody] UpdatePhoneNumberRequest request,
        [FromServices] UpdatePhoneNumberHandler handler,
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
    
    [HttpPut("{userId:guid}/email")]
    public async Task<ActionResult<Guid>> UpdateEmail(
        [FromRoute] Guid userId,
        [FromBody] UpdateEmailRequest request,
        [FromServices] UpdateEmailHandler handler,
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
    
    [HttpPut("{userId:guid}/address")]
    public async Task<ActionResult<Guid>> UpdateAddress(
        [FromRoute] Guid userId,
        [FromBody] UpdateAddressRequest request,
        [FromServices] UpdateAddressHandler handler,
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
        [FromBody] UpdatePhotoRequest request,
        [FromServices] UpdatePhotoHandler handler,
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
    
    [HttpPut("{userId:guid}/companyName")]
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
}