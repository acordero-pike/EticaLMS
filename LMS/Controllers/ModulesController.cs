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
    [Authorize(Roles = "Maestro")]
    public class ModulesController : Controller
    {
        private readonly LMSContext _context;

        public ModulesController(LMSContext context)
        {
            _context = context;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var lMSContext = _context.Modules.Include(Ml => Ml.TipoModuloNavigation);
            return View(await lMSContext.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var Mlmodule = await _context.Modules
                .Include(Ml => Ml.TipoModuloNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (Mlmodule == null)
            {
                return NotFound();
            }

            return View(Mlmodule);
        }

        // GET: Modules/Create
        
        public IActionResult Create()
        {
            ViewData["TipoModulo"] = new SelectList(_context.ModuleTypes, "Uuid", "Nombre");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uuid,Nombre,Descripcion,TipoModulo")] Module Mlmodule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Mlmodule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoModulo"] = new SelectList(_context.ModuleTypes, "Uuid", "Nombre", Mlmodule.TipoModulo);
            return View(Mlmodule);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var Mlmodule = await _context.Modules.FindAsync(id);
            if (Mlmodule == null)
            {
                return NotFound();
            }
            ViewData["TipoModulo"] = new SelectList(_context.ModuleTypes, "Uuid", "Nombre", Mlmodule.TipoModulo);
            return View(Mlmodule);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uuid,Nombre,Descripcion,TipoModulo")] Module Mlmodule)
        {
            if (id != Mlmodule.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Mlmodule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(Mlmodule.Uuid))
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
            ViewData["TipoModulo"] = new SelectList(_context.ModuleTypes, "Uuid", "Nombre", Mlmodule.TipoModulo);
            return View(Mlmodule);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var Mlmodule = await _context.Modules
                .Include(Ml => Ml.TipoModuloNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (Mlmodule == null)
            {
                return NotFound();
            }

            return View(Mlmodule);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Modules == null)
            {
                return Problem("Entity set 'LMSContext.Modules'  is null.");
            }
            var Mlmodule = await _context.Modules.FindAsync(id);
            if (Mlmodule != null)
            {
                _context.Modules.Remove(Mlmodule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(string id)
        {
          return (_context.Modules?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }
    }
}
