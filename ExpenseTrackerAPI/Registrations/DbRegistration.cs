
using ExpenseTracker.Infrasturcture;

namespace ExpenseTracker.Api.Registrations
{
    public class DbRegistration : IWebApplicationBuilderRegistration
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddPersistence(builder.Configuration);
        }
    }
}
