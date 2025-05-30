using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TTM.Host.Controllers.Base;

[ApiController]
public class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}