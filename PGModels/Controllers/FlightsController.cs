﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PGModels.Models;

namespace PGModels.Controllers
{
    public class FlightsController : Controller
    {
        private readonly PilotGuardiansModelsContext _context;

        public FlightsController(PilotGuardiansModelsContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            var pilotGuardiansModelsContext = _context.Flights.Include(f => f.AirCraft);
            return View(await pilotGuardiansModelsContext.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.AirCraft)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["AirCraftId"] = new SelectList(_context.AirCraft, "AirCraftId", "AirCraftId");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeOfFlight,Distance,DateTime,FuelUsed,FlightId,AirCraftId")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirCraftId"] = new SelectList(_context.AirCraft, "AirCraftId", "AirCraftId", flight.AirCraftId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AirCraftId"] = new SelectList(_context.AirCraft, "AirCraftId", "AirCraftId", flight.AirCraftId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TimeOfFlight,Distance,DateTime,FuelUsed,FlightId,AirCraftId")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
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
            ViewData["AirCraftId"] = new SelectList(_context.AirCraft, "AirCraftId", "AirCraftId", flight.AirCraftId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.AirCraft)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Flights == null)
            {
                return Problem("Entity set 'PilotGuardiansModelsContext.Flights'  is null.");
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(string id)
        {
          return (_context.Flights?.Any(e => e.FlightId == id)).GetValueOrDefault();
        }
    }
}
