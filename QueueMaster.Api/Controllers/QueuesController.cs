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
        public async Task<ActionResult> Post(Queue queue)
        {
           await _repository.AddQueue(queue);
            return Ok();
        }

        [HttpPost("item/{id:int}")]
        public async Task<ActionResult> PostQueue(int id, [FromBody]QueueItem item)
        {
            var returnVal = await _repository.InsertQueueItem(id, item);
            if (returnVal >= 0)
            {
                return Ok(returnVal);
            }
            return Problem("Problem Encountered. Data may not have been stored.");
        }

    }
}