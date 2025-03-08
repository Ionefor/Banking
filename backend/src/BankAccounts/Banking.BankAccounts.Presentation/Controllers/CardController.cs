using Banking.Framework;

namespace Banking.BankAccounts.Presentation.Controllers;

public class CardController : ApplicationController
{
    //Account : create, delete, getById, getAllByIdAccount, getAllByIdWallet
    
    // [HttpPost]
    // public async Task<ActionResult<Guid>> Create(
    //     [FromServices] CreateVolunteerHandler handler,
    //     [FromBody] CreateVolunteerRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     var result = await handler.Handle(request.ToCommand(), cancellationToken);
    //
    //     if (result.IsFailure)
    //         return BadRequest(Envelope.Error(result.Error));
    //
    //     return Created("", Envelope.Ok(result.Value));
    // }
}