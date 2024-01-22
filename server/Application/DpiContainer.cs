using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Shared_Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Application
{
    public static class DpiContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Register Http Accessors
            services.AddHttpContextAccessor();
            //Register Auto Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Register Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //Register MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //Register Behaviors => What we want to do before and after a request
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RefreshTokenBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));


            return services;
        }
    }
}
