
using ExpenseTracker.Api.Options;
using Microsoft.OpenApi.Models;

namespace ExpenseTracker.Api.Registrations
{
    public class SwaggerRegistration : IWebApplicationBuilderRegistration
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
