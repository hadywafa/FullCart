using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Security;
using AutoMapper;
using Domain.EFModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Inject MediatR
        
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
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
        public async Task<IActionResult> PlaceOrder([FromQuery] Domain.Enums.PaymentMethod PaymentMethod, [FromQuery] string addressId)
        {

            return Ok();
        }



        [Authorize(Roles = AuthorizeRoles.Customer)]
        [HttpGet("OrderDetails")]
        public async Task<IActionResult> OrderDetails([FromQuery]int id)
        {

            return Ok();
        }

        [HttpGet("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails([FromQuery]int id)
        {
            return Ok();
        }
        
    }
}
