using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Common.Shared_Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.Common.Behaviours
{
    public class RefreshTokenBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
    {
        private readonly Jwt _jwt;
        private readonly IApplicationDbContext _applicationContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;

        public RefreshTokenBehaviour(
            IOptions<Jwt> jwt,
            IApplicationDbContext applicationContext,
            IHttpContextAccessor contextAccessor,
            IIdentityService identityService
        )
        {
            _jwt = jwt.Value;
            _applicationContext = applicationContext;
            _contextAccessor = contextAccessor;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            var accessToken = _contextAccessor.HttpContext.Request.Cookies["_access_token"];
            var refreshToken = _contextAccessor.HttpContext.Request.Cookies["_refresh_token"];
            var idToken = _contextAccessor.HttpContext.Request.Cookies["_id_token"];

            if (accessToken is null && refreshToken is not null)
            {
                var userId = _identityService
                    .DecodeToken(refreshToken)
                    .Result.Identities.FirstOrDefault()
                    ?.Claims.FirstOrDefault()
                    ?.Value;
                var user = await _applicationContext.ApplicationUser.FirstOrDefaultAsync(
                    x => x.Id == userId,
                    cancellationToken: cancellationToken
                );
                var newAccessToken = await _identityService.GenerateAccessToken(user);
                _contextAccessor.HttpContext.Response.Cookies.Append(
                    "_access_token",
                    newAccessToken,
                    new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMinutes(_jwt.AccessTokenDurationInMinutes),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    }
                );
            }
            if (idToken is not null && accessToken is null && refreshToken is null)
            {
                //_identityService.Logout();
                return await next();
            }
            // User is authorized / authorization not required
            return await next();
        }
    }
}
