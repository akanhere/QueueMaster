using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using QueueMaster.Api.Interfaces;
using QueueMaster.Api.Models;
using QueueMaster.Api.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace QueueMaster.Api.Data
{
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueContext _context;
        public QueueRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new QueueContext(settings);
        }

        public async Task AddQueue(Queue queue)
        {
            try
            {
                await _context.Queues.InsertOneAsync(queue);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task<IEnumerable<Queue>> GetQueuesByEstablishment(string tenantId, string establishmentId)
        {
            try
            {
                return await _context.Queues.Find(q => q.TenantId == tenantId && q.EstablishmentId == establishmentId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Queue>> GetQueuesByTenant(string tenantId)
        {
            try
            {
                return await _context.Queues.Find(q => q.TenantId == tenantId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertQueueItem(string queueId, QueueItem queueItem)
        {
            try
            {
                var queue = await _context.Queues.Find(q => q.QueueId == queueId).FirstOrDefaultAsync();
                queue.QueuedItems.Add(queueItem);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task RemoveQueueItem(string queueId, string queueItemId)
        {
            throw new NotImplementedException();
        }
    }
}
