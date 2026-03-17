using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class BaseController : ControllerBase
{
    private ISender _sender = null;
    public ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>(); // Lazy initialization
}
