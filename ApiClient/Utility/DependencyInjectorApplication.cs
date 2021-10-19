using ApiClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiClient.Utility
{
    public static class DependencyInjectorApplication
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddScoped(typeof(IService<>), typeof(ServiceBase<>)); 

            
            //services.AddScoped<IBaseClient, BaseClient>();
            services.AddScoped<IProdutoClient, ProdutoClient>();
        }
    }
}
