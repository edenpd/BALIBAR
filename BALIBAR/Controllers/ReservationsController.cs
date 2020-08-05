using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BALIBAR.Data;
using BALIBAR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BALIBAR.Controllers
{
    public class ReservationsController : Controller
    {

        // Data Members
        private readonly BALIBARContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructors
        public ReservationsController(BALIBARContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Methods
        // GET: Reservations
        [Authorize]
        public async Task<IActionResult> Index(string barName, DateTime fromDate, DateTime toDate)
        {
            IQueryable<Reservation> reservations = _context.Reservation.Include(r => r.Bar);

            // Build the where clause according to the filter parameters:
            // Check is the barname filter was filled
            if (!String.IsNullOrEmpty(barName))
            {
                reservations = reservations.Where(r => r.Bar.Name.Contains(barName));
            }
            
            // Check if the fromDate filter was filled
            if (fromDate != DateTime.MinValue)
            {
                reservations = reservations.Where(r => r.DateTime.Date >= fromDate.Date);
            }

            // Check if the toDate filter was filled
            if (toDate != DateTime.MinValue)
            {
                reservations = reservations.Where(r => r.DateTime.Date <= toDate);
            }

            // Check if the user isn't an admin.
            if (!User.IsInRole("Admin"))
            {

                // Get the connected user.
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

                // Get only reservations that belong to the connected user.
                reservations = reservations.Where(r => r.CustomerId == user.Id);
            }

            List<Reservation> list = await reservations.ToListAsync();

            return View(list);
        }

        // GET: Reservations/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize]
        public IActionResult Create(string? barId)
        {
            // Check if the id field is filled, and if so, hide the bar field.
            ViewBag.showBars = barId == null || barId == "";
            TempData["barId"] = barId;
            TempData["showBars"] = ViewBag.showBars; 

            PopulateBarsDropDownList();
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateTime,AttendeesNum,BarID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                // Get the connected user.
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                reservation.Customer = user;

                // If the bar was already selected beforehand - add it here.
                if (!(bool)TempData["showBars"])
                    reservation.BarID = Int32.Parse(TempData["barId"] as string);

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            PopulateBarsDropDownList();
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,AttendeesNum,BarID")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }

        private void PopulateBarsDropDownList(object selectedBars = null)
        {
            var barQuery = from b in _context.Bar
                           select b;

            ViewBag.BarID = new SelectList(barQuery, "Id", "Name", selectedBars);
        }
    }
}
