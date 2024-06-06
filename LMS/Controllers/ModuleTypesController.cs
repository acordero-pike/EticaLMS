using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Controllers
{
    [Authorize]
    [Authorize(Roles ="Maestro")]
    public class ModuleTypesController : Controller
    {
        private readonly LMSContext _context;

        public ModuleTypesController(LMSContext context)
        {
            _context = context;
        }

        // GET: ModuleTypes
        public async Task<IActionResult> Index()
        {
              return _context.ModuleTypes != null ? 
                          View(await _context.ModuleTypes.ToListAsync()) :
                          Problem("Entity set 'LMSContext.ModuleTypes'  is null.");
        }

        // GET: ModuleTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ModuleTypes == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModuleTypes
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (moduleType == null)
            {
                return NotFound();
            }

            return View(moduleType);
        }

        // GET: ModuleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModuleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uuid,Nombre,Descripcion")] ModuleType moduleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moduleType);
        }

        // GET: ModuleTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ModuleTypes == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModuleTypes.FindAsync(id);
            if (moduleType == null)
            {
                return NotFound();
            }
            return View(moduleType);
        }

        // POST: ModuleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uuid,Nombre,Descripcion")] ModuleType moduleType)
        {
            if (id != moduleType.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleTypeExists(moduleType.Uuid))
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
            return View(moduleType);
        }

        // GET: ModuleTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ModuleTypes == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModuleTypes
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (moduleType == null)
            {
                return NotFound();
            }

            return View(moduleType);
        }

        // POST: ModuleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ModuleTypes == null)
            {
                return Problem("Entity set 'LMSContext.ModuleTypes'  is null.");
            }
            var moduleType = await _context.ModuleTypes.FindAsync(id);
            if (moduleType != null)
            {
                _context.ModuleTypes.Remove(moduleType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleTypeExists(string id)
        {
          return (_context.ModuleTypes?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }
    }
}
