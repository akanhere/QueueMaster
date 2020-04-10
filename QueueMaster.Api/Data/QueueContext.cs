using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QueueMaster.Api.Models;
using QueueMaster.Api.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Data
{

    public class QueueContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly string _collectionName;

        public QueueContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(databaseSettings.Value.Database);
                _collectionName = databaseSettings.Value.QueueCollectionName;
            }
        }

        public IMongoCollection<Queue> Queues
        {
            get
            {
                return _database.GetCollection<Queue>(_collectionName);
            }
        }

    }
}
