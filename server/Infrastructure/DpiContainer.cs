using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts;
using Application.Common.Shared_Models;
using Domain.EFModels;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class DpiContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            #region Register Dependencies objects for infrastructure

            //services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            //Register Domain Event Service 
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            //Register IdentityService
            services.AddTransient<IIdentityService, IdentityService>();

            #endregion

            #region Register DB Context

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            #endregion

            #region Register IdentityUser to my DB Context

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            #endregion


            #region Binding Jwt Configuration in appsetting.json to Jwt.cs class

            services.Configure<Jwt>(configuration.GetSection("JWT"));

            #endregion

            #region Register JWT Options for Authentication and validation Token

            services.AddAuthentication(defaultOptions =>
                {
                    defaultOptions.DefaultScheme = "Cookie";
                    defaultOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Bearer
                    defaultOptions.DefaultChallengeScheme = "Bearer";
                }).AddCookie("Cookie", options =>
                {
                    options.Cookie.Name = "Hw_cookie";
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddJwtBearer("Bearer", extraOptions =>
                {
                    extraOptions.RequireHttpsMetadata = false;
                    extraOptions.SaveToken = true;

                    //Validate Client token options
                    extraOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //options
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        //comparision
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };

                    extraOptions.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["_access_token"];
                            context.Token = token;
                            return Task.CompletedTask;
                        }
                    };
                });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader());
            //});
         
            #endregion

            return services;
        }
    }
}