using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace ApiClient.Utility
{
    public static class DependencyInjectorConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration != null)
            {
                var settingsSection = configuration.GetSection("Environment");
                if (settingsSection != null)
                {
//                    services.Configure<AppSettings>(settingsSection);
                }

            }
        }
    }
}
