using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueMaster.Api.Models
{
    public class QueueItem
    {
        public string QueueItemId { get; set; }
        public string QueuedUserDisplayName { get; set; }
        public string QueuedUserId { get; set; }
        public int QueueAtPosition { get; set; }
        public int QueueCurrentPosition { get; set; }
        public DateTime QueuedAtTime { get; set; }
        public DateTime QueuedOutAtTime { get; set; }
        public string StatusDescription { get; set; }
        public string QRCodeUrl { get; set; }
    }
}
