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
        /// <param name="update">Update</param>
        /// <returns></returns>
        bool ExecuteUpdate(Update update);

        /// <summary>
        /// Select an update.
        /// </summary>
        /// <param name="updates">Update list</param>
        /// <returns>Selected update</returns>
        Update SelectUpdate(IList<Update> updates);
    }
}
