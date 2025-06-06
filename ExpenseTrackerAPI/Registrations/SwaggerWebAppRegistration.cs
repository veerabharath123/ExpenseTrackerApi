﻿
using Asp.Versioning.ApiExplorer;
namespace ExpenseTracker.Api.Registrations
{
    public class SwaggerWebAppRegistration : IWebApplicationRegistration
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
                }
            });
        }
    }
}
