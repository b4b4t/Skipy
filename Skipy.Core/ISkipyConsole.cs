using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Skipy.Core
{
    public interface ISkipyConsole
    {
        /// <summary>
        /// Service collection
        /// </summary>
        IServiceCollection ServiceCollection { get; }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Start the application console.
        /// </summary>
        /// <param name="args">Arguments</param>
        public void Start(string[] args);
    }
}
