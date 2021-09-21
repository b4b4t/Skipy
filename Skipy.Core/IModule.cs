using Microsoft.Extensions.DependencyInjection;

namespace Skipy.Core
{
    public interface IModule
    {
        string Name { get; }

        void ConfigureServices(IServiceCollection serviceCollection);
    }
}
