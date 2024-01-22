using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.User.Commands.RegisterUser
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(request => request.UserRegisterDtoReq.Email).EmailAddress();
            RuleFor(request => request.UserRegisterDtoReq.Roles.Count).GreaterThan(0);
            RuleFor(request => request.UserRegisterDtoReq.Password).NotEmpty();
            RuleFor(request => request.UserRegisterDtoReq.Password).MinimumLength(6);
            RuleFor(request => request.UserRegisterDtoReq.FirstName).NotEmpty();
            RuleFor(request => request.UserRegisterDtoReq.LastName).NotEmpty();
        }
    }
}
