using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailSalon.DataAccess; 
using NailSalon.Models;

public class BookingsController : Controller
{
    private readonly ApplicationDbContext _context; 

    public BookingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Bookings/List
    public IActionResult List()
    {
        var bookings = _context.Bookings.ToList();
        return View(bookings);
    }

    // GET: Bookings/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("StaffName,Time,CustomerName,ServiceName")] Booking booking)
    {
        if (ModelState.IsValid)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        return View(booking);
    }

    // GET: Bookings/Edit/
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var booking = _context.Bookings.Find(id);

        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // POST: Bookings/Edit/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,StaffName,Time,CustomerName,ServiceName")] Booking booking)
    {
        if (id != booking.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(booking);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(List));
        }
        return View(booking);
    }

    // GET: Bookings/Delete/
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var booking = _context.Bookings.Find(id);

        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // POST: Bookings/Delete/
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var booking = _context.Bookings.Find(id);
        _context.Bookings.Remove(booking);
        _context.SaveChanges();
        return RedirectToAction(nameof(List));
    }

    private bool BookingExists(int id)
    {
        return _context.Bookings.Any(e => e.Id == id);
    }


}

