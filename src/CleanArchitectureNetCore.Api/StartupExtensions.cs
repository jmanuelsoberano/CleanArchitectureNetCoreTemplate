﻿using CleanArchitectureNetCore.Api.Services;
using CleanArchitectureNetCore.Api.Utility;
using CleanArchitectureNetCore.Application;
using CleanArchitectureNetCore.Application.Contracts;
using CleanArchitectureNetCore.DataAccess.Commands;
using CleanArchitectureNetCore.DataAccess.Queries;
using CleanArchitectureNetCore.Identity;
using CleanArchitectureNetCore.Infrastructure;
using Microsoft.OpenApi.Models;

namespace CleanArchitectureNetCore.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        AddSwagger(builder.Services);

        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddDataAccessServices();
        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddMailServices(builder.Configuration);

        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture Net Core API");
            });
        }

        //app.UseHttpsRedirection();

        //app.UseRouting();

        app.UseAuthentication();

        // app.UseCustomExceptionHandler();

        app.UseCors("Open");

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Clean Architecture Net Core API"
            });
            
            c.TagActionsBy(api =>
            {
                var relativePath = api.RelativePath.Split("/");
                return string.Join(".", relativePath.Take(relativePath.Length - 1));
            });

            c.CustomSchemaIds(x => x.FullName);

            c.OperationFilter<FileResultContentTypeOperationFilter>();
        });
    }
}