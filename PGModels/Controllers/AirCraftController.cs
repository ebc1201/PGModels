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
    public class AirCraftController : Controller
    {
        private readonly PilotGuardiansModelsContext _context;

        public AirCraftController(PilotGuardiansModelsContext context)
        {
            _context = context;
        }

        // GET: AirCraft
        public async Task<IActionResult> Index()
        {
              return _context.AirCraft != null ? 
                          View(await _context.AirCraft.ToListAsync()) :
                          Problem("Entity set 'PilotGuardiansModelsContext.AirCraft'  is null.");
        }

        // GET: AirCraft/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AirCraft == null)
            {
                return NotFound();
            }

            var airCraft = await _context.AirCraft
                .FirstOrDefaultAsync(m => m.AirCraftId == id);
            if (airCraft == null)
            {
                return NotFound();
            }

            return View(airCraft);
        }

        // GET: AirCraft/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AirCraft/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,AirCraftId,EngineType,Capacity")] AirCraft airCraft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airCraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airCraft);
        }

        // GET: AirCraft/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AirCraft == null)
            {
                return NotFound();
            }

            var airCraft = await _context.AirCraft.FindAsync(id);
            if (airCraft == null)
            {
                return NotFound();
            }
            return View(airCraft);
        }

        // POST: AirCraft/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Type,AirCraftId,EngineType,Capacity")] AirCraft airCraft)
        {
            if (id != airCraft.AirCraftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airCraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirCraftExists(airCraft.AirCraftId))
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
            return View(airCraft);
        }

        // GET: AirCraft/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AirCraft == null)
            {
                return NotFound();
            }

            var airCraft = await _context.AirCraft
                .FirstOrDefaultAsync(m => m.AirCraftId == id);
            if (airCraft == null)
            {
                return NotFound();
            }

            return View(airCraft);
        }

        // POST: AirCraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AirCraft == null)
            {
                return Problem("Entity set 'PilotGuardiansModelsContext.AirCraft'  is null.");
            }
            var airCraft = await _context.AirCraft.FindAsync(id);
            if (airCraft != null)
            {
                _context.AirCraft.Remove(airCraft);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirCraftExists(string id)
        {
          return (_context.AirCraft?.Any(e => e.AirCraftId == id)).GetValueOrDefault();
        }
    }
}
