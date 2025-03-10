using Banking.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Framework.Controller;

[ApiController]
[Route("[controller]")]
public partial class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}