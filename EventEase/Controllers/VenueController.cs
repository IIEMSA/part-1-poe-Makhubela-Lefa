using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venue venue)
        {


            if (ModelState.IsValid)
            {

                _context.Add(venue);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            return View(venue);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(m => m.VenueID == id);

            if (id == null)
            {
                return NotFound();
            }
            return View(venue);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(m => m.VenueID == id);

            if (id == null)
            {
                return NotFound();
            }
            return View(venue);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            _context.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(m => m.VenueID == id);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Venue updatedVenue)
        {
            if (id != updatedVenue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedVenue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists( updatedVenue.VenueID))
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

            return View(updatedVenue);
        }
    }
}
