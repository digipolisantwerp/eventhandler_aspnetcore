using Digipolis.Eventhandler;
using Digipolis.Eventhandler.Message;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Startup
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register your business services here, e.g. services.AddTransient<IMyService, MyService>();
            services.AddTransient<IEventHandler, EventHandler>();

            return services;
        }
    }
}