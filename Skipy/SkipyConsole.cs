using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skipy.Commands;
using Skipy.Core;
using Skipy.Infrastructure;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.IO;
using System.Reflection;

namespace Skipy
{
    public class SkipyConsole : ISkipyConsole
    {
        private static readonly string CurrentDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private readonly IServiceCollection _serviceCollection;
        private IConfiguration _configuration;

        /// <summary>
        /// Service collection
        /// </summary>
        public IServiceCollection ServiceCollection { get => _serviceCollection; }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get => _configuration; }

        public SkipyConsole(IConfiguration configuration = null, IServiceCollection serviceCollection = null)
        {
            _serviceCollection = serviceCollection ?? new ServiceCollection();
            _configuration = configuration ?? GetConfiguration();
        }

        /// <inheritdoc cref="ISkipyConsole.Start(string[], Action{IConfigurator})" />
        public void Start(string[] args, Action<IConfigurator> configuration = null)
        {
            string header = @"
                 (`-').-> <-.(`-')    _       _  (`-')            
                 ( OO)_    __( OO)   (_)      \-.(OO )      .->   
                (_)--\_)  '-'. ,--.  ,-(`-')  _.'    \  ,--.'  ,-.
                /    _ /  |  .'   /  | ( OO) (_...--'' (`-')'.'  /
                \_..`--.  |      /)  |  |  ) |  |_.' | (OO \    / 
                .-._)   \ |  .   '  (|  |_/  |  .___.'  |  /   /) 
                \       / |  |\   \  |  |'-> |  |       `-/   /`  
                 `-----'  `--' '--'  `--'    `--'         `--'    
                ";

            AnsiConsole.MarkupLine(header);

            var registrar = new TypeRegistrar(_serviceCollection);
            registrar.RegisterInstance(typeof(EmptyCommandSettings), new EmptyCommandSettings());

            // Command application
            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                config.AddCommand<StatusCommand>("status")
                    .WithDescription("Display the list of the updates with their status.");
                config.AddCommand<UpdateCommand>("update")
                    .WithDescription("Update the database to an update");
            });

            if (configuration is not null)
            {
                app.Configure(configuration);
            }

            try
            {
                app.Run(args);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }
        }

        /// <summary>
        /// Get the configuration of the application.
        /// By default, load the configuration from the appsettings.json file.
        /// </summary>
        /// <returns>Configuration</returns>
        protected virtual IConfiguration GetConfiguration()
        {
            // Configuration
            return new ConfigurationBuilder()
                .SetBasePath(CurrentDirectoryPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
