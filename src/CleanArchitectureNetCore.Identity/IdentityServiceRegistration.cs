using CleanArchitectureNetCore.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CleanArchitectureNetCore.Application.Contracts.Identity.Roles;
using CleanArchitectureNetCore.Application.Contracts.Identity.Users;
using CleanArchitectureNetCore.Identity.Services;
using CleanArchitectureNetCore.Identity.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitectureNetCore.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CleanArchitectureNetCoreIdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };

                    o.Events = new JwtBearerEvents
                    {
                        //OnAuthenticationFailed = c =>
                        //{
                        //    c.NoResult();
                        //    c.Response.StatusCode = 500;
                        //    c.Response.ContentType = "text/plain";
                        //    return c.Response.WriteAsync(c.Exception.ToString());
                        //},
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize("401 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                        // OnAuthenticationFailed = context =>
                        // {
                        //     // context.HandleResponse();
                        //     context.Response.StatusCode = 401;
                        //     context.Response.ContentType = "application/json";
                        //     var result = JsonSerializer.Serialize("401 Unauthorized");
                        //     return context.Response.WriteAsync(result);
                        // },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize("403 Forbidden");
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
