using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Skipy.Core;
using Skipy.Core.DTO;
using Spectre.Console;

namespace Skipy.EntityFramework.Provider
{
    public class EntityFrameworkProvider<T> : IUpdateProvider where T : DbContext
    {
        private readonly T _dbContext;

        public EntityFrameworkProvider(T dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ExecuteUpdate(Update update)
        {
            if (update is null)
            {
                throw new InvalidOperationException("The migration was not found");
            }

            _dbContext.GetInfrastructure().GetService<IMigrator>().Migrate(update.Name);

            return true;
        }

        public IList<Update> LoadUpdateList()
        {
            IEnumerable<string> pendingUpdates = _dbContext.Database.GetPendingMigrations();
            IEnumerable<string> appliedUpdates = _dbContext.Database.GetAppliedMigrations();

            var updates = new List<Update>();
            string parentId = null;

            foreach (string migrationName in appliedUpdates.OrderBy(m => m))
            {
                string id = GetId(migrationName);
                string name = GetName(migrationName, id);

                updates.Add(ToUpdate(id, name, parentId, true));

                parentId = id;
            }

            foreach (string migrationName in pendingUpdates.OrderBy(m => m))
            {
                string id = GetId(migrationName);
                string name = GetName(migrationName, id);
                
                updates.Add(ToUpdate(id, name, parentId, false));

                parentId = id;
            }

            return updates.Where(m => m is not null)
                .ToList();
        }


        public Update SelectUpdate(IList<Update> updates)
        {
            var prompt = new SelectionPrompt<Update>()
                .UseConverter(update => update.Name)
                .Title("Select a migration :")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more migrations)[/]")
                .AddChoices(updates);

            return AnsiConsole.Prompt(prompt);
        }

        /// <summary>
        /// Get name from migration name.
        /// </summary>
        /// <param name="migrationName">Migration name</param>
        /// <param name="id">Migration identifier</param>
        /// <returns>Short name of the migration</returns>
        private static string GetName(string migrationName, string id)
        {
            return migrationName.Replace($"{id}_", string.Empty);
        }

        /// <summary>
        /// Get Id from migration name.
        /// </summary>
        /// <param name="migrationName">Migration name</param>
        /// <returns>Migration id</returns>
        private static string GetId(string migrationName)
        {
            return migrationName.Split('_')[0];
        }

        /// <summary>
        /// Generate an update from a migration.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Migration name</param>
        /// <param name="isInstalled">If the update is installed</param>
        /// <returns>Update</returns>
        private static Update ToUpdate(string id, string name, string parentId, bool isInstalled)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return new Update()
            {
                Id = id,
                Name = name,
                IsInstalled = isInstalled,
                ParentId = parentId
            };
        }
    }
}
