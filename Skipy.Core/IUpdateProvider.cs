using System.Collections.Generic;
using Skipy.Core.DTO;

namespace Skipy.Core
{
    public interface IUpdateProvider
    {
        /// <summary>
        /// Load the update list.
        /// </summary>
        /// <returns>List of updates</returns>
        IList<Update> LoadUpdateList();

        /// <summary>
        /// Set the database to a specified update.
        /// </summary>
        /// <param name="updateId">Update Id</param>
        /// <returns></returns>
        bool ExecuteUpdate(string updateId);
    }
}
