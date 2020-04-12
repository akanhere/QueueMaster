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
            catch (Exception ex)
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

        public async Task<IEnumerable<Queue>> GetQueuesByEstablishment(int tenantId, int establishmentId)
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

        public async Task<IEnumerable<Queue>> GetQueuesByTenant(int tenantId)
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

        public async Task<IEnumerable<Queue>> GetQueueById(int queueId)
        {
            try
            {
                return await _context.Queues.Find(q => q.QueueId == queueId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> InsertQueueItem(int queueId, QueueItem queueItem)
        {
            try
            {
                var items = _context.Queues.Find(b => b.QueueId == queueId).ToList();
                var nextId = items.SelectMany(q => q.QueuedItems).Any() ? (items.SelectMany(q => q.QueuedItems).Max(m => m.QueueItemId) + 1) : 1;


                queueItem.QueueItemId = nextId;
                queueItem.QueuedAtTime = DateTime.UtcNow;
                queueItem.StatusDescription = new String("Queued");

                var arrayFilter = Builders<Queue>.Filter.Eq("QueueId", queueId);
                var arrayUpdate = Builders<Queue>.Update.AddToSet("QueuedItems", queueItem);



                UpdateResult x = await _context.Queues.UpdateOneAsync(arrayFilter, arrayUpdate);
                if (x.IsAcknowledged)
                {
                    return Convert.ToInt32(x.ModifiedCount);
                }
                return -1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task RemoveQueueItem(int queueId, int queueItemId)
        {
            throw new NotImplementedException();
        }
    }
}
