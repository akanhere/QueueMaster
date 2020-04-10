using QueueMaster.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Interfaces
{
    public interface IQueueRepository
    {
        Task<IEnumerable<Queue>> GetQueuesByTenant(string tenantId);
        Task<IEnumerable<Queue>> GetQueuesByEstablishment(string tenantId, string establishmentId);
        Task AddQueue(Queue queue);
        Task InsertQueueItem(string queueId, QueueItem queueItem);
        Task RemoveQueueItem(string queueId, string queueItemId);
    }
}
