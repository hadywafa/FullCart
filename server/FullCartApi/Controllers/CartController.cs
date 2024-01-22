using System.Threading.Tasks;
using Application.Common.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromQuery] int proId, [FromQuery] int count)
        {
            return StatusCode(200);
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity(
            [FromQuery] int proId,
            [FromQuery] int count
        )
        {
            return Ok("item quantity updated successfully");
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpDelete("eemove")]
        public async Task<IActionResult> RemoveFromCart([FromQuery] int proId)
        {
            return StatusCode(200);
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpGet("getCartPrice")]
        public async Task<IActionResult> GetPrice()
        {
            return Ok();
        }
    }
}
