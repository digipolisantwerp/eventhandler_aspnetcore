using Microsoft.Extensions.DependencyInjection;

namespace Api.Startup
{
    public static class AutoMapperRegistration
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            BusinessEntitiesToDataContracts();
            AgentToBusinessEntities();
			AgentToDataContracts();
            
            return services;
        }

        private static void BusinessEntitiesToDataContracts()
        {
         
        }

        private static void AgentToBusinessEntities()
        {

		}

        private static void AgentToDataContracts()
        {
            
        }
    }
}