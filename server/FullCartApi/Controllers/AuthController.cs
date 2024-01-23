using System.Threading.Tasks;
using Application.Common.Shared_Models;
using Application.Features.User.Commands.LogoutUser;
using Application.Features.User.Commands.RegisterUser;
using Application.Features.User.Commands.SignInWithIdv;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDtoReq userDto)
        {
            var result = await _mediator.Send(
                new RegisterCommand() { UserRegisterDtoReq = userDto }
            );
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.Error);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _mediator.Send(
                new LoginCommand() { Email = email, Password = password }
            );
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.Error);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.Send(new LogoutCommand());
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.Error);
        }
    }
}
