using LMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Globalization;

namespace LMS.Controllers
{
    [Authorize]
    [Authorize(Roles = "Estudiante")]
    public class EticaController : Controller
    {
        private readonly LMSContext _context = new LMSContext();
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

        public IActionResult ExamenM1(string id)
        {
            TempData["Detalle"] = id;
            // Linq validar si tiene examen calificado 

            return View();
        }

        public IActionResult Calificar(string id)
        {
            
            Test Examen = new Test();

            Examen.Usuario = User.Claims.Last().Value;
            Examen.Nota = 70;
            Examen.Calificado = true;
            Examen.Modulo = id;
          _context.Tests.Add(Examen);
            _context.SaveChanges();
            TempData["Detalle"] = Examen.Nota;
            return View();
        }


    }
}
