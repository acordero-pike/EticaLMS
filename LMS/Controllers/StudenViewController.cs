using LMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    [Authorize]
    [Authorize(Roles = "Estudiante")]
    public class StudenViewController : Controller
    {
        private readonly LMSContext _context;

        public StudenViewController(LMSContext context)
        {
            _context = context;
        }
        // GET: StudenView
        public async Task<IActionResult> Index()
        {
            return _context.ModuleTypes != null ?
                        View(await _context.ModuleTypes.Include(x => x.Modules).ToListAsync()) :
                        Problem("Entity set 'LMSContext.ModuleTypes'  is null.");
        }

        // GET: StudenView/Details/5
        public ActionResult Moduleview(string id)
        {
            var x  = _context.Modules.Include(x => x.TipoModuloNavigation).FirstOrDefault(x => x.Uuid == id);
            string Action = "";
            string sModule = "";

            if(x.TipoModuloNavigation.Nombre == "Codigo de Etica")
            {
                Action = "Etica";
            }
            else if(x.TipoModuloNavigation.Nombre == "Codigo de Conducta")
            {
                Action = "Conducta";
            }

            if(x.Nombre.TrimEnd()=="Modulo 1")
            {
                sModule = "Module1";
            }
            else if (x.Nombre.TrimEnd() == "Modulo 2")
            {
                sModule = "Module2";
            }
            else if (x.Nombre.TrimEnd() == "Modulo 3")
            {
                sModule = "Module3";
            }

            return RedirectToAction(sModule, Action);
        }

        // GET: StudenView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudenView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudenView/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudenView/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudenView/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudenView/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
