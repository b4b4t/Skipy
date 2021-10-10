using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Skipy.Core
{
    public class Module<T> : IModule where T : class, IUpdateProvider
    {
        /// <inheritdoc cref="IModule.ConfigureServices(IServiceCollection, IConfiguration)" />
        public virtual void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IUpdateProvider, T>();
        }
    }
}
