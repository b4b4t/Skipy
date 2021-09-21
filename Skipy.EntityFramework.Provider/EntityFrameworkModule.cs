using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skipy.Core;

namespace Skipy.EntityFramework.Provider
{
    public class EntityFrameworkModule<T> : IModule where T : DbContext
    {
        private readonly IConfiguration _configuration;

        public virtual string Name => throw new NotImplementedException();

        public virtual void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Database connection
            serviceCollection.AddDbContext<T>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
