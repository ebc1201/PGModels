using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PGModels.Models;

namespace PGModels.Controllers
{
    public class PilotsController : Controller
    {
        private readonly PilotGuardiansModelsContext _context;

        public PilotsController(PilotGuardiansModelsContext context)
        {
            _context = context;
        }

        // GET: Pilots
        public async Task<IActionResult> Index()
        {
            var pilotGuardiansModelsContext = _context.Pilots.Include(p => p.Flight).Include(p => p.Person);
            return View(await pilotGuardiansModelsContext.ToListAsync());
        }

        // GET: Pilots/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Pilots == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilots
                .Include(p => p.Flight)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.LicenseNumber == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        // GET: Pilots/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id");
            return View();
        }

        // POST: Pilots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicenseNumber,TotalHours,Certifications,Ratings,FlightId,PersonId")] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pilot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", pilot.FlightId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", pilot.PersonId);
            return View(pilot);
        }

        // GET: Pilots/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Pilots == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilots.FindAsync(id);
            if (pilot == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", pilot.FlightId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", pilot.PersonId);
            return View(pilot);
        }

        // POST: Pilots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LicenseNumber,TotalHours,Certifications,Ratings,FlightId,PersonId")] Pilot pilot)
        {
            if (id != pilot.LicenseNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pilot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotExists(pilot.LicenseNumber))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", pilot.FlightId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", pilot.PersonId);
            return View(pilot);
        }

        // GET: Pilots/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Pilots == null)
            {
                return NotFound();
            }

            var pilot = await _context.Pilots
                .Include(p => p.Flight)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.LicenseNumber == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        // POST: Pilots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Pilots == null)
            {
                return Problem("Entity set 'PilotGuardiansModelsContext.Pilots'  is null.");
            }
            var pilot = await _context.Pilots.FindAsync(id);
            if (pilot != null)
            {
                _context.Pilots.Remove(pilot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilotExists(string id)
        {
          return (_context.Pilots?.Any(e => e.LicenseNumber == id)).GetValueOrDefault();
        }
    }
}
