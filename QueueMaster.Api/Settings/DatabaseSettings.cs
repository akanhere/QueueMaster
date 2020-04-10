using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string QueueCollectionName { get; set; }
    }
}
