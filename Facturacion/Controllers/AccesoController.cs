using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.Models;
using Microsoft.AspNetCore.Http;

namespace Facturacion.Controllers
{
    // INTENTO DE COMMIT
    public class AccesoController : Controller
    {
        private readonly FacturacionDbContext _context;

        public AccesoController(FacturacionDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ID,Nombre_Usuario,Contraseña,ID_Vendedor")] Usuario user)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios.Include(u => u.Vendedor)
                    .FirstOrDefaultAsync(u => u.Nombre_Usuario == user.Nombre_Usuario && u.Contraseña == user.Contraseña);

                if (usuario != null)
                {
                    HttpContext.Session.SetString("Usuario", usuario.Nombre_Usuario);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "El usuario o la contraseña que ha ingresado no son válidos.";
                }
            }

            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToAction("Login");
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}
