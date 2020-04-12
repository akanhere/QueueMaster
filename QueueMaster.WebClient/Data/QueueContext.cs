using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QueueMaster.WebClient.Models;

namespace QueueMaster.WebClient.Data
{
    public class QueueContext : DbContext
    {
        public QueueContext (DbContextOptions<QueueContext> options)
            : base(options)
        {
        }

        public DbSet<QueueMaster.WebClient.Models.Queue> Queue { get; set; }
    }
}
