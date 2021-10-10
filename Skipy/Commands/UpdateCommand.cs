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
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Update update;
            IList<Update> updates = null;

            AnsiConsole.Status()
                .Start("Loading migrations ...", ctx =>
                {
                    updates = _updateProvider.LoadUpdateList();
                });

            if (!string.IsNullOrEmpty(settings.Id))
            {
                update = updates.SingleOrDefault(u => u.Id == settings.Id);
            }
            else if (!string.IsNullOrEmpty(settings.Name))
            {
                update = updates.SingleOrDefault(u => u.Name == settings.Name);
            }
            else
            {
                if (updates is null || updates.Count == 0)
                {
                    AnsiConsole.WriteLine("[red]No migration found[/]");

                    return 0;
                }

                update = _updateProvider.SelectUpdate(updates);
            }

            AnsiConsole.Status()
                .Start("Updating ...", ctx =>
                {
                    ExecuteUpdate(update);
                });

            return 0;
        }

        /// <summary>
        /// Apply the update.
        /// </summary>
        /// <param name="update">Update</param>
        private void ExecuteUpdate(Update update)
        {
            try
            {
                if (_updateProvider.ExecuteUpdate(update))
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
