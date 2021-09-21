using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Skipy.Commands;
using Skipy.Core;
using Skipy.Infrastructure;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Skipy
{
    public class Program
    {
        public static int Main(string[] args)
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

            var serviceCollection = new ServiceCollection();
            LoadModule(serviceCollection);

            var registrar = new TypeRegistrar(serviceCollection);

            // Command application
            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                config.AddCommand<StatusCommand>("status")
                    .WithDescription("Display the list of the updates with their status.");
                config.AddCommand<UpdateCommand>("update")
                    .WithDescription("Update the database to an update");
            });

            return app.Run(args);
        }

        /// <summary>
        /// Load the module assembly. 
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        private static void LoadModule(IServiceCollection serviceCollection)
        {
            Type moduleType = typeof(IModule);
            string modulePath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Module";
            string[] files = Directory.GetFiles(modulePath, "*.dll");

            Assembly moduleAssembly;

            foreach (string file in files)
            {
                moduleAssembly = AssemblyHelper.LoadAssembly(file);

                if (moduleAssembly is null)
                {
                    continue;
                }

                foreach (TypeInfo typeInfo in moduleAssembly.DefinedTypes)
                {
                    if (typeInfo.ImplementedInterfaces.Contains(moduleType))
                    {
                        ConfigureModuleServices(serviceCollection, moduleAssembly);

                        return;
                    }
                }
            }

            throw new InvalidOperationException("No module found");
        }

        /// <summary>
        /// Configure the module services.
        /// </summary>
        /// <param name="serviceCollection">Services collection.</param>
        /// <param name="assembly">Module assembly</param>
        private static void ConfigureModuleServices(
            IServiceCollection serviceCollection,
            Assembly assembly
        ){
            Type moduleType = typeof(IModule);
            Type[] types = assembly.GetTypes();

            for (int j = 0; j < types.Length; j++)
            {
                Type type = types[j];

                if (moduleType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                {
                    IModule module = Activator.CreateInstance(type) as IModule;
                    module.ConfigureServices(serviceCollection);
                }
            }
        }
    }
}
