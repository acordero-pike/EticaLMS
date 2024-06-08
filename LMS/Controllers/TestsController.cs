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
    public class TestsController : Controller
    {
        private readonly LMSContext _context;

        public TestsController(LMSContext context)
        {
            _context = context;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
            var lMSContext = _context.Tests.Include(t => t.ModuloNavigation).Include(t => t.UsuarioNavigation);
            return View(await lMSContext.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.ModuloNavigation)
                .Include(t => t.UsuarioNavigation)
                  .Include(t => t.ModuloNavigation.TipoModuloNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {

            var m = from a in _context.ModuleTypes join c in _context.Modules on a.Uuid equals c.TipoModulo
                    select new
                    {
                        Uuid = c.Uuid,
                        Nombre = a.Nombre +" "+ c.Nombre
                    };
            ViewData["Modulo"] = new SelectList(m, "Uuid", "Nombre");
            ViewData["Usuario"] = new SelectList(_context.Users, "Uuid", "Nombre");
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uuid,Usuario,Modulo,Nota,Calificado")] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var m = from a in _context.ModuleTypes
                    join c in _context.Modules on a.Uuid equals c.TipoModulo
                    select new
                    {
                        Uuid = c.Uuid,
                        Nombre = a.Nombre + " " + c.Nombre
                    };
            ViewData["Modulo"] = new SelectList(m, "Uuid", "Nombre", test.Modulo);
           
            ViewData["Usuario"] = new SelectList(_context.Users, "Uuid", "Nombre", test.Usuario);
            return View(test);
        }

        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            var m = from a in _context.ModuleTypes
                    join c in _context.Modules on a.Uuid equals c.TipoModulo
                    select new
                    {
                        Uuid = c.Uuid,
                        Nombre = a.Nombre + " " + c.Nombre
                    };
            ViewData["Modulo"] = new SelectList(m, "Uuid", "Nombre", test.Modulo);
            ViewData["Usuario"] = new SelectList(_context.Users, "Uuid", "Nombre", test.Usuario);
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uuid,Usuario,Modulo,Nota,Calificado")] Test test)
        {
            if (id != test.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Uuid))
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
            var m = from a in _context.ModuleTypes
                    join c in _context.Modules on a.Uuid equals c.TipoModulo
                    select new
                    {
                        Uuid = c.Uuid,
                        Nombre = a.Nombre + " " + c.Nombre
                    };
            ViewData["Modulo"] = new SelectList(m, "Uuid", "Nombre", test.Modulo);
            ViewData["Usuario"] = new SelectList(_context.Users, "Uuid", "Nombre", test.Usuario);
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.ModuloNavigation)
                .Include(t => t.UsuarioNavigation)
                .Include(t => t.ModuloNavigation.TipoModuloNavigation)
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Tests == null)
            {
                return Problem("Entity set 'LMSContext.Tests'  is null.");
            }
            var test = await _context.Tests.FindAsync(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(string id)
        {
          return (_context.Tests?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }
    }
}
