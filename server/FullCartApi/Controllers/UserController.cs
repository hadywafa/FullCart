using System.Threading.Tasks;
using Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Inject MediatR

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion


        [Authorize(Roles = AuthorizeRoles.Customer)]
        [Route("Addresses")]
        [HttpGet]
        public async Task<ActionResult> GetAllAddresses()
        {
            return Ok();
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress([FromBody] object _address)
        {
            return StatusCode(200);
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] object newAddress)
        {
            return StatusCode(200, new { MessagePack = "Address is Updated Successfully" });
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress([FromQuery] int addressId)
        {
            return StatusCode(200, new { MessagePack = "Address is removed Successfully" });
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPut("UpdateUserName")]
        public async Task<IActionResult> UpdateUserName(
            [FromQuery] string first,
            [FromQuery] string last
        )
        {
            return StatusCode(200, new { message = "Your name updated Successfully" });
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] object passObj)
        {
            return StatusCode(200, new { message = "Password updated Successfully" });
        }
    }
}
