using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Common.Shared_Models;
using Domain.EFModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using CSharpFunctionalExtensions;

namespace Application.Features.User.Commands.SignInWithIdv
{
    public class LoginCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInWithIdvCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Jwt _jwt;

        public SignInWithIdvCommandHandler(
            UserManager<ApplicationUser> userManager,
            IOptions<Jwt> jwt,
            IIdentityService identityService,
            IHttpContextAccessor contextAccessor
        )
        {
            _userManager = userManager;
            _identityService = identityService;
            _contextAccessor = contextAccessor;
            _jwt = jwt.Value;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result.Failure("Email or Password is incorrect!");
            }

            var accessToken = await _identityService.GenerateAccessToken(user);
            _contextAccessor.HttpContext.Response.Cookies.Append(
                "_access_token",
                accessToken,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(_jwt.AccessTokenDurationInMinutes),
                    HttpOnly = true,
                    Secure = false, // for local development
                    SameSite = SameSiteMode.None
                }
            );

            var refreshToken = await _identityService.GenerateRefreshToken(user);
            _contextAccessor.HttpContext.Response.Cookies.Append(
                "_refresh_token",
                refreshToken,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(_jwt.RefreshTokenDurationInHours),
                    HttpOnly = true,
                    Secure = false, // for local development
                    SameSite = SameSiteMode.None
                }
            );

            var idToken = JsonSerializer.Serialize(
                new UserAuthDtoRes
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }
            );
            _contextAccessor.HttpContext.Response.Cookies.Append(
                "_id_token",
                idToken,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(100),
                    HttpOnly = false,
                    Secure = false, // for local development
                    SameSite = SameSiteMode.None
                }
            );
            return Result.Success();
        }
    }
}
