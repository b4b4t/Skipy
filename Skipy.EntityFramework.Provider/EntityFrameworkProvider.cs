using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Skipy.Core;
using Skipy.Core.DTO;

namespace Skipy.EntityFramework.Provider
{
    public class EntityFrameworkProvider<T> : IUpdateProvider where T : DbContext
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkProvider(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ExecuteUpdate(string updateId)
        {
            if (string.IsNullOrEmpty(updateId))
            {
                return false;
            }

            _dbContext.GetInfrastructure().GetService<IMigrator>().Migrate(updateId);

            return true;
        }

        public IList<Update> LoadUpdateList()
        {
            IList<Update> pendingUpdates = _dbContext.Database.GetPendingMigrations()
                .Select(mig => ToUpdate(mig, false))
                .ToList();

            IList<Update> appliedUpdates = _dbContext.Database.GetPendingMigrations()
                .Select(mig => ToUpdate(mig, false))
                .ToList();

            return appliedUpdates.Concat(pendingUpdates).ToList();
        }

        /// <summary>
        /// Generate an update from a migration.
        /// </summary>
        /// <param name="migrationName">Migration name</param>
        /// <param name="isInstalled">If the update is installed</param>
        /// <returns>Update</returns>
        private static Update ToUpdate(string migrationName, bool isInstalled)
        {
            return new Update()
            {
                Id = migrationName,
                Name = migrationName,
                IsInstalled = isInstalled
            };
        }
    }
}
