using Microsoft.Extensions.DependencyInjection;

namespace App.Infra.IoC
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
            //services.AddTransient<ISmsSender, AuthSMSMessageSender>();
            //services.AddScoped<IUser, AspNetUser>();
        }
    }
}
