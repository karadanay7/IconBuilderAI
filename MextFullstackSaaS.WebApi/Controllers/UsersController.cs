using MediatR;
using MextFullstackSaaS.Application;

using MextFullstackSaaS.Application.Features.Orders.Commands.Add;

using MextFullstackSaaS.Application.Features.Orders.Commands.Delete;
using MextFullstackSaaS.Application.Features.Orders.Queries.GetAll;
using MextFullstackSaaS.Application.Features.Orders.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MextFullstackSaaS.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
      private readonly ISender _mediatr;
      public UsersController(ISender mediatr)
      {
          _mediatr = mediatr;
      }

      [HttpGet("profile")]
      public async Task<IActionResult> GetProfileAsync( CancellationToken cancellationToken)
      {
          return Ok(await _mediatr.Send(new UserGetProfileQuery(), cancellationToken));
      }
    }
}