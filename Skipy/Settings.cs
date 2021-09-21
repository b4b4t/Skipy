using Spectre.Console.Cli;

namespace Skipy
{
    public class Settings : CommandSettings
    {
        /// <summary>
        /// Update ID.
        /// </summary>
        [CommandArgument(0, "[Id]")]
        public string Id { get; set; }
    }
}
