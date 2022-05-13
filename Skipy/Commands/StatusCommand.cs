using System.Collections.Generic;
using Skipy.Core;
using Skipy.Core.DTO;
using Skipy.Core.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Skipy.Commands
{
    public class StatusCommand : Command
    {
        private readonly IUpdateProvider _updateProvider;

        public StatusCommand(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }

        public override int Execute(CommandContext context)
        {
            AnsiConsole.Status()
                .Start("Loading ...", ctx =>
                {
                    IList<Update> updateList = _updateProvider.LoadUpdateList();

                    var table = new Table();

                    table.AddColumns("ID", "Name", "Status");

                    foreach (Update update in updateList.OrderByUpdate())
                    {
                        table.AddRow(update.Id, update.Name, update.IsInstalled ? "[green]Installed[/]" : "[red]Not installed[/]");
                    }

                    AnsiConsole.Write(table);
                });

            return 0;
        }
    }
}
