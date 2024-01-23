using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Common.Exceptions;
using Application.Common.Shared_Models;
using Domain.EFModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        #region Inject UserManager Context in Authntication Service

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Jwt _jwt;
        private readonly IHttpContextAccessor _contextAccessor;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IOptions<Jwt> jwt
        )
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _jwt = jwt.Value;
        }

        #endregion

        public async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            //define token description          <== SecurityTokenDescriptor
            //1-define user Claims              <==
            //2-define your signature           <== SigningCredentials ( SymmetricSecurityKey , SecurityAlgorithms )
            //3-Create token obj                <== JwtSecurityTokenHandler
            //4-Convert token Obj to string     <== JwtSecurityTokenHandler

            #region create claims [ main information that will be encrypted in token ]

            //add db user claims to token claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            //add roles to token claims
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
                .Union(userClaims)
                .Union(roleClaims);

            #endregion


            var signature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            //var signature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdfasdgqreasdfasdgaerfsdfwerfqwerfwf"));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_jwt.AccessTokenDurationInMinutes),
                SigningCredentials = new SigningCredentials(
                    signature,
                    SecurityAlgorithms.HmacSha256
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenObj);
        }

        public Task<string> GenerateRefreshToken(ApplicationUser user)
        {
            var signature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            //var signature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdfarsfgqergqerfgqerf"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[] { new Claim(JwtRegisteredClaimNames.Sub, user.Id) }
                ),
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                Expires = DateTime.Now.AddHours(_jwt.RefreshTokenDurationInHours),
                SigningCredentials = new SigningCredentials(
                    signature,
                    SecurityAlgorithms.HmacSha256
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
            return Task.Run(() => tokenHandler.WriteToken(tokenObj));
        }

        public Task<string> GenerateStateToken(ApplicationUser user)
        {
            var signature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                Expires = DateTime.Now.AddHours(_jwt.RefreshTokenDurationInHours),
                SigningCredentials = new SigningCredentials(
                    signature,
                    SecurityAlgorithms.HmacSha256
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
            return Task.Run(() => tokenHandler.WriteToken(tokenObj));
        }

        public Task<ClaimsPrincipal> DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenObj = tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwt.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken securityToken
            );
            return Task.Run(() => tokenObj);
        }

        public void Logout()
        {
            var cookies = _contextAccessor.HttpContext?.Request.Cookies;
            if (cookies.Count == 0)
                return;
            foreach (var cookie in cookies)
            {
                //_contextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);

                _contextAccessor.HttpContext.Response.Cookies.Append(
                    cookie.Key,
                    "",
                    new CookieOptions()
                    {
                        Expires = DateTimeOffset.Now.AddHours(-1),
                        SameSite = SameSiteMode.None,
                        Secure = true,
                        Path = "/"
                    }
                );
            }
        }
    }
}
