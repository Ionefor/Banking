using Banking.Core.Models;
using Banking.Framework;
using Banking.UserAccounts.Presentation.Requests.Profile;
using Microsoft.AspNetCore.Mvc;

namespace Banking.UserAccounts.Presentation.Controllers;

public class UserProfilesController : ApplicationController
{
    // Accounts : getAllUsers, getUserById, getByFullNameUsers, getAllIndividualAcc,  getAllCorparateAcc, 
    //pagination, filtering
    
    //
    // [HttpGet]
    // public async Task<ActionResult<Guid>> GetAll(
    //     [FromServices] GetVolunteersWithPaginationHandler handler,
    //     [FromQuery] GetUsersWithPaginationRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     var query = request.ToQuery();
    //
    //     var response = await handler.Handle(query, cancellationToken);
    //
    //     return Ok(response);
    // }
    
    // [HttpGet("{volunteerId:guid}")]
    // public async Task<ActionResult<Guid>> GetById(
    //     [FromServices] GetVolunteerByIdHandler handler,
    //     [FromRoute] Guid volunteerId,
    //     CancellationToken cancellationToken)
    // {
    //     var query = new GetVolunteerByIdQuery(volunteerId);
    //
    //     var result = await handler.Handle(query, cancellationToken);
    //     
    //     if (result.IsFailure)
    //         return BadRequest(Envelope.Error(result.Error));
    //     
    //     return Ok(result.Value);
    // }
    //
}