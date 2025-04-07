using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Index
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public async Task<IActionResult> Create(Booking newBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(newBooking);
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(n => n.BookingID == id);
            if (id == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(n => n.BookingID == id);
            if (id == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a booking exists
        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(n => n.BookingID == id);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingToEdit = await _context.Bookings.FindAsync(id);
            if (bookingToEdit == null)
            {
                return NotFound();
            }
            return View(bookingToEdit);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Booking newBooking)
        {
            if (id != newBooking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(newBooking.BookingID))
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

            return View(newBooking);
        }
    }
}
