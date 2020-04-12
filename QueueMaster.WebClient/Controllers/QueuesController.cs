using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QueueMaster.WebClient.Data;
using QueueMaster.WebClient.Models;

namespace QueueMaster.WebClient.Controllers
{
    public class QueuesController : Controller
    {
        private readonly QueueContext _context;

        public QueuesController(QueueContext context)
        {
            _context = context;
        }

        // GET: Queues
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Queue.ToListAsync());

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44331/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/queues/1").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Queue> data = JsonConvert.DeserializeObject<List<Queue>>(stringData);
                return View(data);
            }
        }

        // GET: Queues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queue
                .FirstOrDefaultAsync(m => m.QueueId == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // GET: Queues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Queues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,EstablishmentId,EstablishmentName,QueueId,ManagerName,ManagerContact,QueueDescription,QueueMaxItems,AverageWaitTimePerUser")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(queue);
        }

        // GET: Queues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queue.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }
            return View(queue);
        }

        // POST: Queues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,EstablishmentId,EstablishmentName,QueueId,ManagerName,ManagerContact,QueueDescription,QueueMaxItems,AverageWaitTimePerUser")] Queue queue)
        {
            if (id != queue.QueueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueueExists(queue.QueueId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(queue);
        }

        // GET: Queues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queue
                .FirstOrDefaultAsync(m => m.QueueId == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // POST: Queues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queue = await _context.Queue.FindAsync(id);
            _context.Queue.Remove(queue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueueExists(int id)
        {
            return _context.Queue.Any(e => e.QueueId == id);
        }
    }
}
