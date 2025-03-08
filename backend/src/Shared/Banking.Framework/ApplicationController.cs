using Banking.Core.Models;
using Banking.Core.Response;
using Banking.Framework.Extensions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Framework;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
    
    protected async Task<ActionResult<TResponse>> HandleRequest<TRequest, TCommand, TResponse>(
        Guid userId,
        TRequest request,
        Func<TRequest, Guid, TCommand> createCommand,
        Func<TCommand, CancellationToken, Task<Result<TResponse, ErrorList>>> handleCommand,
        Func<ErrorList, ObjectResult> createErrorResult,
        CancellationToken cancellationToken)
    {
        var command = createCommand(request, userId);
        var result = await handleCommand(command, cancellationToken);
        
        if (result.IsFailure)
            return createErrorResult(result.Error);

        return Ok(result.Value);
    }
    
    protected async Task<ActionResult<TResponse>> HandleRequest<TRequest, TCommand, TResponse>(
        TRequest request,
        Func<TRequest, TCommand> createCommand,
        Func<TCommand, CancellationToken, Task<Result<TResponse, ErrorList>>> handleCommand,
        CancellationToken cancellationToken)
    {
        var command = createCommand(request);
        var result = await handleCommand(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    protected async Task<ActionResult<TResponse>> HandleRequest<TCommand, TResponse>(
        Guid userId,
        Func<Guid, TCommand> createCommand,
        Func<TCommand, CancellationToken, Task<Result<TResponse, ErrorList>>> handleCommand,
        Func<ErrorList, ObjectResult> createErrorResult,
        CancellationToken cancellationToken)
    {
        var command = createCommand!(userId);
        var result = await handleCommand(command, cancellationToken);
        
        if (result.IsFailure)
            return createErrorResult(result.Error);

        return Ok(result.Value);
    }
    
    protected async Task<ActionResult> HandleRequest<TRequest, TCommand>(
        TRequest request,
        Func<TRequest, TCommand> createCommand,
        Func<TCommand, CancellationToken, Task<UnitResult<ErrorList>>> handleCommand,
        CancellationToken cancellationToken)
    {
        var command = createCommand(request);
        var result = await handleCommand(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
}