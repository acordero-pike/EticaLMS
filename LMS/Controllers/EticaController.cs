using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Authorize]
    [Authorize(Roles = "Estudiante")]
    public class EticaController : Controller
    {
        public IActionResult Module1()
        {   
            return View();
        }
        public IActionResult Module2()
        {
            return View();
        }
        public IActionResult Module3()
        {
            return View();
        }
    }
}
