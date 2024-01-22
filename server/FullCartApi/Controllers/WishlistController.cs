using System.Threading.Tasks;
using Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        #region Inject MediatR
        
        private readonly IMediator _mediator;
        public WishlistController(IMediator mediator)
        {
            _mediator = mediator;
        }      

        #endregion


        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpPost("Add")]
        public async Task<IActionResult> AddToWishlist([FromQuery]int proId )
        {
            return StatusCode(200);
        }

        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpDelete("Remove")]
        public  async Task<IActionResult> RemoveFromWishlist([FromQuery]int proId)
        {
            return Ok("item Removed successfully from your wishlist");
        }

    }
}
