using Microsoft.AspNetCore.Hosting;

namespace Api.Shared.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsVagrant(this IHostingEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment("vagrant");
        }
    }
}
