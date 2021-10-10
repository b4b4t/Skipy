using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Skipy.Core
{
    public interface IModule
    {
        /// <summary>
        /// Add the module services to the console service collection.
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        /// <param name="configuration">Configuration</param>
        void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}
