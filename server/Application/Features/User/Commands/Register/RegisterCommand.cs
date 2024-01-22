using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Security;
using Application.Common.Shared_Models;
using CSharpFunctionalExtensions;
using Domain.EFModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.Commands.RegisterUser
{
    //Request
    public class RegisterCommand : IRequest<Result>
    {
        public UserRegisterDtoReq UserRegisterDtoReq { get; set; }
    }

    //Request Handler
    public class RegisterUserCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken
        )
        {
            #region Validate User input Model

            if (
                await _userManager.FindByEmailAsync(request.UserRegisterDtoReq.Email) is not null
                || await _userManager.FindByNameAsync(request.UserRegisterDtoReq.UserName)
                    is not null
            )
            {
                throw new ExistedException("this Email is Already Existed");
            }

            #endregion

            var user = new ApplicationUser
            {
                FirstName = request.UserRegisterDtoReq.FirstName,
                LastName = request.UserRegisterDtoReq.LastName,
                UserName = request.UserRegisterDtoReq.UserName,
                //need to confirm mail
                Email = request.UserRegisterDtoReq.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, request.UserRegisterDtoReq.Password);

            #region Validate Result then return errors

            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var e in result.Errors)
                {
                    errors += $"{e.Description}, ";
                }
                return Result.Failure(errors);
            }

            #endregion

            //User is Created Successfully
            //Assign User to Role
            foreach (string role in request.UserRegisterDtoReq.Roles.Distinct())
            {
                if (
                    role != AuthorizeRoles.Admin
                    || role != AuthorizeRoles.Customer
                    || role != AuthorizeRoles.Seller
                    || role != AuthorizeRoles.Shipper
                )
                    continue;

                await _userManager.AddToRoleAsync(user, role);

                #region assign user to type [admin / customer / seller / shipper] depend on its role

                switch (role)
                {
                    case AuthorizeRoles.Admin:

                        {
                            var admin = new Admin();
                            user.Admin = admin;
                            await _userManager.UpdateAsync(user);
                        }
                        break;
                    case AuthorizeRoles.Customer:

                        {
                            var customer = new Customer();
                            user.Customer = customer;
                            await _userManager.UpdateAsync(user);
                        }
                        break;
                    case AuthorizeRoles.Seller:

                        {
                            var seller = new Seller();
                            user.Seller = seller;
                            await _userManager.UpdateAsync(user);
                        }
                        break;
                    case AuthorizeRoles.Shipper:

                        {
                            var shipper = new Shipper();
                            user.Shipper = shipper;
                            await _userManager.UpdateAsync(user);
                        }
                        break;
                }

                #endregion
            }
            return Result.Success();
        }
    }
}
