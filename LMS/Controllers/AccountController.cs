using LMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Controllers
{
   public class AccountController : Controller
    {
        public List<Claim> claims = new List<Claim>();
        private readonly LMSContext _context = new LMSContext();



        [HttpGet("Login")]
        public IActionResult Login()
        {

            return View();
        }

        [Route("error/handle/{errorCode}")]
        public IActionResult Handle(int errorCode)
        {
            // Do something and return a view or a content
            return View();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> validate(string usuario, string password)
        {


            User val = _context.Users.Where(a => a.Usuario == usuario && a.Contraseña == password).Any() ? _context.Users.Include(s => s.TipoUsuarioNavigation).Where(a => a.Usuario == usuario && a.Contraseña == password).First():new User() ;
            if (val.Uuid!="")
            {
               
                claims.Add(new Claim("username", val.Nombre)); // guardamos el nombre de quien se logea
              
                claims.Add(new Claim(ClaimTypes.Role, val.TipoUsuarioNavigation.Nombre));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, val.Uuid)); //guardamos el tipo de peticion 
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); // asignamos esa peticicon a un esquema de cookies
                var claimprincipal = new ClaimsPrincipal(claimIdentity); // la volvemos peticion principal


                await HttpContext.SignInAsync(claimprincipal); // cremos la cookie de autentificacion

                return RedirectToAction("Index", "Home"); // redireccion a un pagina 
            }
            else
            {
                 


                return RedirectToAction("Index", "Error", new { data = "Error de Log in", data2 = "Usuario o Contraseña Incorrecto!!" });
                // si el usuario no es valido envia un badrequest como respuesta


            }


        }
       
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(
     CookieAuthenticationDefaults.AuthenticationScheme); //elimina la cookie creada 
            return RedirectToAction("Index", "Home"); // regresa a una pagina especifica 
        }
    }
}