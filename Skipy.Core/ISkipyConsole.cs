using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

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
        IConfiguration Configuration { get; }

        /// <summary>
        /// Start the application console.
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <param name="configuration">Configuration for the command line application</param>
        void Start(string[] args, Action<IConfigurator> configuration = null);
    }
}
