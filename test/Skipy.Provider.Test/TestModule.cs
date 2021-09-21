using Microsoft.Extensions.DependencyInjection;
using Skipy.Core;

namespace Skipy.Provider.Test
{
    public class TestModule : IModule
    {
        public string Name => "Test module";

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUpdateProvider, TestProvider>();
        }
    }
}
