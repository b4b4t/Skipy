using System;
using Microsoft.Extensions.DependencyInjection;

namespace Skipy.Core
{
    public class Module<T> : IModule where T : class, IUpdateProvider, new() 
    {
        public virtual string Name => throw new NotImplementedException();

        public virtual void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUpdateProvider, T>();
        }
    }
}
