namespace ExpenseTracker.Api.Registrations
{
    public interface IWebApplicationRegistration:IRegistration
    {
        public void RegisterPipelineComponents(WebApplication app);
    }
}
