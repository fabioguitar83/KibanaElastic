using KibanaSerilog.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KibanaSerilog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddRequest user)
        {
            var response = await _mediator.Send(user);
            
            return StatusCode(response.StatusCode.GetHashCode(),response.Content);
        }
    }
}