using System.Collections.Generic;
using System.Linq;
using Skipy.Core.DTO;

namespace Skipy.Core.Utils
{
    public static class ListExtensions
    {
        /// <summary>
        /// Sort the elements by update.
        /// </summary>
        /// <param name="updates">Update list</param>
        /// <returns>Sorted update list.</returns>
        public static IList<Update> OrderByUpdate(this IList<Update> updates)
        {
            // First parent
            Update update = updates.SingleOrDefault(upd => upd.ParentId is null);

            if (update is null)
            {
                return new List<Update>();
            }

            IList<Update> sortedList = new List<Update>(updates.Count) { update };

            while (update != null)
            {
                update = updates.SingleOrDefault(upd => upd.ParentId == update.Id);

                if (update != null)
                {
                    sortedList.Add(update);
                }
            }

            return sortedList;
        }
    }
}
