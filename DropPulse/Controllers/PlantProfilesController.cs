using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DropPulse.Models;

namespace DropPulse.Controllers
{
    public class PlantProfilesController : Controller
    {
        private readonly DroppulseContext _context;

        public PlantProfilesController(DroppulseContext context)
        {
            _context = context;
        }

        // GET: PlantProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlantProfiles.ToListAsync());
        }

        // GET: PlantProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantProfile = await _context.PlantProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantProfile == null)
            {
                return NotFound();
            }

            return View(plantProfile);
        }

        // GET: PlantProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlantProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MinSoilMoistureThreshold,MaxSoilMoistureThreshold")] PlantProfile plantProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plantProfile);
        }

        // GET: PlantProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantProfile = await _context.PlantProfiles.FindAsync(id);
            if (plantProfile == null)
            {
                return NotFound();
            }
            return View(plantProfile);
        }

        // POST: PlantProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MinSoilMoistureThreshold,MaxSoilMoistureThreshold")] PlantProfile plantProfile)
        {
            if (id != plantProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantProfileExists(plantProfile.Id))
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
            return View(plantProfile);
        }

        // GET: PlantProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantProfile = await _context.PlantProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantProfile == null)
            {
                return NotFound();
            }

            return View(plantProfile);
        }

        // POST: PlantProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantProfile = await _context.PlantProfiles.FindAsync(id);
            if (plantProfile != null)
            {
                _context.PlantProfiles.Remove(plantProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantProfileExists(int id)
        {
            return _context.PlantProfiles.Any(e => e.Id == id);
        }
    }
}
