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

    // GET: Bookings/Edit
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

    // POST: Bookings/Edit
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

    private bool BookingExists(int id)
    {
        throw new NotImplementedException();
    }

    // GET: Bookings/Delete
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

    // POST: Users/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteUserConfirmed(int id)
    {
        var user = _context.Customers.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        
        var hasBookings = _context.Bookings.Any(b => b.CustomerName == user.Name);

        if (hasBookings)
        {
           
            ModelState.AddModelError(string.Empty, "Cannot be deleted!");
            return View("Delete", user);
        }

        _context.Customers.Remove(user);
        _context.SaveChanges();

        return RedirectToAction(nameof(CustomerList));
    }

    // POST: Bookings/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteBookingConfirmed(int id)
    {
        var booking = _context.Bookings.Find(id);

        if (booking == null)
        {
            return NotFound();
        }

        var hasService = _context.Services.Any(s => s.Name == booking.ServiceName);

        if (hasService)
        {
            
            ModelState.AddModelError(string.Empty, "Cannot be deleted!");
            return View("Delete", booking);
        }
        _context.Bookings.Remove(booking);
        _context.SaveChanges();

        return RedirectToAction(nameof(BookingList));
    }

    // POST: Services/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteServiceConfirmed(int id)
    {
        var service = _context.Services.Find(id);

        if (service == null)
        {
            return NotFound();
        }

       
        var hasBookings = _context.Bookings.Any(b => b.ServiceName == service.Name);

        if (hasBookings)
        {
            
            ModelState.AddModelError(string.Empty, "Cannot be deleted!");
            return View("Delete", service);
        }

        
        _context.Services.Remove(service);
        _context.SaveChanges();

        return RedirectToAction(nameof(ServiceList));
    }

    // GET: Bookings/CustomerList
    public IActionResult CustomerList()
    {
        var customers = _context.Customers.ToList();
        return View(customers);
    }

    // GET: Bookings/BookingList
    public IActionResult BookingList()
    {
        var bookings = _context.Bookings.ToList();
        return View(bookings);
    }

    // GET: Bookings/ServiceList
    public IActionResult ServiceList()
    {
        var services = _context.Services.ToList();
        return View(services);
    }


}

