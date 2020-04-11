﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Models
{
    public class Queue
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public int TenantId { get; set; }
        public int EstablishmentId { get; set; }

        public string EstablishmentName { get; set; }
        public int QueueId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerContact { get; set; }
        public string QueueDescription { get; set; }
        public int QueueMaxItems { get; set; }
        public int AverageWaitTimePerUser { get; set; }
        public ICollection<QueueItem> QueuedItems { get; set; }

    }
}
