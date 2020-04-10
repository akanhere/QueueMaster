using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Models
{
    public class Queue
    {
        public string TenantId { get; set; }
        public string EstablishmentId { get; set; }
        public string QueueId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerContact { get; set; }
        public string QueueDescription { get; set; }
        public int QueueMaxItems { get; set; }
        public int AverageWaitTimePerUser { get; set; }
        public ICollection<QueueItem> QueuedItems { get; set; }

    }
}
