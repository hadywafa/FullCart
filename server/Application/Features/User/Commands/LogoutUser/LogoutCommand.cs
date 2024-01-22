using Application.Common.Contracts;
using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.LogoutUser
{
    public class LogoutCommand : IRequest<Result> { }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {
        private IIdentityService _identityService { get; }

        public LogoutCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            _identityService.Logout();
            await Task.CompletedTask;
            return Result.Success();
        }
    }
}
