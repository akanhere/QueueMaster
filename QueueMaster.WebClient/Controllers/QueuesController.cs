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
                HttpResponseMessage response = client.GetAsync("/api/queues/tenant/1").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Queue> data = JsonConvert.DeserializeObject<List<Queue>>(stringData);
                return View(data);
            }
        }



        public async Task<IActionResult> AddQueueItem(int id)
        {
            ViewBag.QueueId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQueueItem(int queueId, QueueItem queueItem)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44331/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var content = JsonConvert.SerializeObject(queueItem);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("/api/queues/item/" + queueId, byteContent).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                //var data = JsonConvert.DeserializeObject<List<long>>(stringData);
                return RedirectToAction("Details", "Queues", new { id = queueId });
            }
        }

        // GET: Queues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var queue = await _context.Queue
            //    .FirstOrDefaultAsync(m => m.QueueId == id);
            //if (queue == null)
            //{
            //    return NotFound();
            //}
            //return View(queue);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44331/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response =await client.GetAsync("/api/queues/1");
                string stringData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Queue>>(stringData);
                return View(data.FirstOrDefault());
            }
        }
        public IActionResult ItemEdit(int? id)
        {
            return Content("Item Edited" + id);
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
