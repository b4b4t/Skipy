using System.Collections.Generic;
using Skipy.Core;
using Skipy.Core.DTO;

namespace Skipy.Provider.Test
{
    public class TestProvider : IUpdateProvider
    {
        public bool ExecuteUpdate(string updateId)
        {
            return true;
        }

        public IList<Update> LoadUpdateList()
        {
            return new List<Update>()
            {
                new Update() { Id = "ID2", Name = "Update2", ParentId = "ID1", IsInstalled = true },
                new Update() { Id = "ID5", Name = "Update5", ParentId = "ID4", IsInstalled = true },
                new Update() { Id = "ID1", Name = "Update1", ParentId = null, IsInstalled = true },
                new Update() { Id = "ID6", Name = "Update6", ParentId = "ID5", IsInstalled = false },
                new Update() { Id = "ID7", Name = "Update7", ParentId = "ID6", IsInstalled = false },
                new Update() { Id = "ID3", Name = "Update3", ParentId = "ID2", IsInstalled = true },
                new Update() { Id = "ID4", Name = "Update4", ParentId = "ID3", IsInstalled = true }
            };
        }
    }
}
