using System;
using System.Collections.Generic;
using System.Linq;
using Skipy.Core;
using Skipy.Core.DTO;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Skipy.Commands
{
    public class UpdateCommand : Command<Settings>
    {
        private readonly IUpdateProvider _updateProvider;

        public UpdateCommand(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            string id = settings.Id;
            IList<Update> updates = _updateProvider.LoadUpdateList();

            if (id is null || !updates.Any(upd => upd.Id == id))
            {
                AnsiConsole.MarkupLine($"[red]Missing update corresponding to the given ID[/]");

                return 0;
            }

            AnsiConsole.Status()
                .Start("Updating ...", ctx =>
                {
                    ExecuteUpdate(id);
                });

            return 0;
        }

        /// <summary>
        /// Apply the update.
        /// </summary>
        /// <param name="id">Update ID</param>
        private void ExecuteUpdate(string id)
        {
            try
            {
                if (_updateProvider.ExecuteUpdate(id))
                {
                    AnsiConsole.MarkupLine($"[green]Update done[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]Update was not applied[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Update cannot be applied[/]");
                AnsiConsole.WriteException(ex);
            }
        }
    }
}
