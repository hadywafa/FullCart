using System.Security.Claims;
using System.Threading.Tasks;
using Application.Common.Shared_Models;
using Domain.EFModels;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Contracts
{
    public interface IIdentityService
    {
        //VmAuthUser        => Response     => UserAuthDtoRes
        //VmRegisterUser    => Request      => UserRegisterDtoReq
        //VmSignInUser      => Request      => String Email , String Password

        //Task<UserAuthDtoRes> RegisterAsync(UserRegisterDtoReq model);
        Task<string> GenerateAccessToken(ApplicationUser user);
        Task<string> GenerateRefreshToken(ApplicationUser user);
        Task<ClaimsPrincipal> DecodeToken(string token);
        void Logout();
    }
}
