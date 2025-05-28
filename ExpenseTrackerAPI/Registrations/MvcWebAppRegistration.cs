using ExpenseTracker.Domain.Constants;
using Microsoft.AspNetCore.HttpOverrides;

namespace ExpenseTracker.Api.Registrations
{

    public class MvcWebAppRegistration : IWebApplicationRegistration
    {
        private readonly string _policyName = "ExpenseTrackerPolicy";
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseRequestLocalization();

            app.UseExceptionHandler(_ => { });

            app.UseCors(_policyName);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
