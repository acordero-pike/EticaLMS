using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
         public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contactenos(int? id)
        {
            if (id != null)
            {
                TempData["succes"] = "Mensaje enviado Correctamente";
                return View();
            }
            return View();
        }
        [HttpPost("contacto")]
        public IActionResult mensaje(string Nombre, string Correo, string Telefono, string Mensaje)
        {/*
            
            MailMessage email = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            email.To.Add(new MailAddress(correo));
            email.From = new MailAddress("webdesingaya@gmail.com");
            email.Subject = "Notificación ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = "Por este medio ya que se ha solicitado la recuperacion de la clave de usuario en cual se utiliza en la aplicaion web de la Empresa KSE la cual es =" + clave + "| Firma Equipo de Seguridad de KSEDOCUMENTACION";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Host = ("smtp.gmail.com");
            client.Port = 587;

            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            string cr = "skecorp01@gmail.com";
            string cl = "ALC123456";
            client.Credentials = new System.Net.NetworkCredential(cr, cl);


            try
            {
                client.Send(email);
                email.Dispose();
                TempData["Error"] = "Correo electrónico fue enviado satisfactoriamente.";
                RedirectToAction("Login", new { er = TempData["Error"] });

            }

            catch (Exception ex)
            {
                TempData["Error"] = "Error enviando correo electrónico: " + ex.Message;
            }
            */

            return RedirectToAction("Contactenos", "Home", new { id = 1 });

        }

    }
}
