namespace Skipy.Core.Utils
{
    public static class SkipyExtensions
    {
        /// <summary>
        /// Add a provider to update the database.
        /// </summary>
        /// <typeparam name="T">Updater type</typeparam>
        /// <param name="serviceCollection">Service collection</param>
        /// <param name="type"></param>
        public static void AddProvider<T>(this ISkipyConsole console) where T : class, IModule, new()
        {
            IModule module = new T();
            module.ConfigureServices(console.ServiceCollection, console.Configuration);
        }
    }
}
