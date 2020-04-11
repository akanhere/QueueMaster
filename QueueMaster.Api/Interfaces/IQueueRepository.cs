using QueueMaster.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Interfaces
{
    public interface IQueueRepository
    {
        Task<IEnumerable<Queue>> GetQueuesByTenant(int tenantId);
        Task<IEnumerable<Queue>> GetQueuesByEstablishment(int tenantId, int establishmentId);
        Task AddQueue(Queue queue);
        Task InsertQueueItem(int queueId, QueueItem queueItem);
        Task RemoveQueueItem(int queueId, int queueItemId);
    }
}
