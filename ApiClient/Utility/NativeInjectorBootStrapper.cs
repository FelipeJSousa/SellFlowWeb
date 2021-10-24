using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiClient.Utility
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjectorConfiguration.Register(services, configuration);
            DependencyInjectorApplication.Register(services);
        }
    }
}
