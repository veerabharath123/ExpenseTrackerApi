using ExpenseTracker.Api.Class;
using ExpenseTracker.Api.ExceptionHandling;
using ExpenseTracker.Application;
using ExpenseTracker.Domain.Constants;
using ExpenseTracker.Infrasturcture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExpenseTracker.Api.Registrations
{
    public class MvcRegistration : IWebApplicationBuilderRegistration
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.AddServerHeader = false;
            });

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => ApplyJwtBearerOptions(x, builder.Configuration));

            builder.Services.AddExceptionHandler<AppExceptionHandler>()
            .AddControllers(options =>
            {
                options.Filters.Add<ValidationModelAttribute>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services
                .AddEndpointsApiExplorer()
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddSingleton<IAuthorizationHandler, DbPermissionHandler>();
        }
        private static void ApplyJwtBearerOptions(JwtBearerOptions options, ConfigurationManager configuration)
        {
            var signInKey = configuration["JwtAuth:SigningKey"] ?? string.Empty;
            var tokenKey = configuration["JwtAuth:EncryptKey"] ?? string.Empty;

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey)),
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidIssuer = configuration["JwtAuth:Issuer"],
                ValidAudience = configuration["JwtAuth:Audience"],
                ClockSkew = TimeSpan.Zero,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
            };
        }
    }
    
}
