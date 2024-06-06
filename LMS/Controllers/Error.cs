using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class Error : Controller
    {
        public IActionResult Index(string? data, string[]? data2)
        {
            if (data == null)
            {
                TempData["ErrorViewModel"] = "Error en el llamado de la pagina";
                return View();
            }
            else
            {
                TempData["ErrorViewModel"] = data;
                TempData["Detalle"] = data2[0];
                return View();
            }
        }

        public IActionResult _NotFound()
        {
            return View();
        }

        }
}
