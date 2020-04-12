using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueMaster.Api.Interfaces;
using QueueMaster.Api.Models;

namespace QueueMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueRepository _repository;

        public QueuesController(IQueueRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<IEnumerable<Queue>> GetByTenantId(int tenantId)
        {
            return await _repository.GetQueuesByTenant(tenantId);
        }

        [HttpGet("{queueId}")]
        public async Task<IEnumerable<Queue>> Get(int queueId)
        {
            return await _repository.GetQueueById(queueId);
        }

        [HttpPost]
        public void Post(Queue queue)
        {
            _repository.AddQueue(queue);
        }

        [HttpPost("item/{id:int}")]
        public void PostQueue(int id, [FromBody]QueueItem item)
        {
            _repository.InsertQueueItem(id, item);
        }

    }
}