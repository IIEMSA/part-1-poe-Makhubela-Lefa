using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action to display list of events
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        // Create GET action
        public IActionResult Create()
        {
            return View();
        }

        // Create POST action
        [HttpPost]
        public async Task<IActionResult> Create(Event currentEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currentEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(currentEvent);
        }

        // Details action to display event details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentEvent = await _context.Events.FirstOrDefaultAsync(m => m.EventID == id);
            if (currentEvent == null)
            {
                return NotFound();
            }

            return View(currentEvent);
        }

        // Delete GET action to confirm event deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentEvent = await _context.Events.FirstOrDefaultAsync(m => m.EventID == id);
            if (currentEvent == null)
            {
                return NotFound();
            }

            return View(currentEvent);
        }

        // Delete POST action to delete the event
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var currentEvent = await _context.Events.FindAsync(id);

            if (currentEvent != null)
            {
                _context.Events.Remove(currentEvent);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Edit GET action to get the event data for editing
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentEvent = await _context.Events.FindAsync(id);
            if (currentEvent == null)
            {
                return NotFound();
            }

            return View(currentEvent);
        }

        // Edit POST action to save the updated event
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event currentEvent)
        {
            if (id != currentEvent.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currentEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(currentEvent.EventID))
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

            return View(currentEvent);
        }

        // Private helper method to check if an event exists
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
