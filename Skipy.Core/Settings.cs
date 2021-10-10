using Spectre.Console.Cli;

namespace Skipy.Core
{
    public class Settings : CommandSettings
    {
        /// <summary>
        /// Update ID.
        /// </summary>
        [CommandArgument(0, "[Id]")]
        public string Id { get; set; }

        /// <summary>
        /// Update Name.
        /// </summary>
        [CommandArgument(0, "[Name]")]
        public string Name { get; set; }
    }
}
