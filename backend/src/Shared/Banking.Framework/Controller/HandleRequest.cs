﻿using Banking.Framework.Extensions;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Framework.Controller;

public partial class ApplicationController : ControllerBase
{
    protected async Task<ActionResult<TResponse>> HandleCommand<TRequest, TCommand, TResponse>(
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
    
    protected async Task<ActionResult<TResponse>> HandleCommand<TRequest, TCommand, TResponse>(
        Guid cId,
        Guid aId,
        TRequest request,
        Func<TRequest, Guid, Guid, TCommand> createCommand,
        Func<TCommand, CancellationToken, Task<Result<TResponse, ErrorList>>> handleCommand,
        Func<ErrorList, ObjectResult> createErrorResult,
        CancellationToken cancellationToken)
    {
        var command = createCommand(request, cId, aId);
        var result = await handleCommand(command, cancellationToken);
        
        if (result.IsFailure)
            return createErrorResult(result.Error);

        return Ok(result.Value);
    }
    
    protected async Task<ActionResult<TResponse>> HandleCommand<TRequest, TCommand, TResponse>(
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
    
    protected async Task<ActionResult<TResponse>> HandleCommand<TCommand, TResponse>(
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
    
    protected async Task<ActionResult> HandleCommand<TRequest, TCommand>(
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