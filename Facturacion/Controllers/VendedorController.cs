using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.Models;
using Facturacion.Filters;

namespace Facturacion.Controllers
{
    [VerificarSession]
    public class VendedorController : Controller
    {
        private readonly FacturacionDbContext _context;

        public VendedorController(FacturacionDbContext context)
        {
            _context = context;
        }

        // GET: Vendedor
        public async Task<IActionResult> Index()
        {
            var facturacionDbContext = _context.Vendedores.Include(v => v.Estado);
            return View(await facturacionDbContext.ToListAsync());
        }

        // GET: Vendedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedores
                .Include(v => v.Estado)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");

            return View(vendedor);
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View();
        }

        // POST: Vendedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Porc_Comision,ID_Estado,Usuario")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();

                Usuario user = new Usuario();
                user.Nombre_Usuario = vendedor.Usuario.Nombre_Usuario;
                user.Contraseña = vendedor.Usuario.Contraseña;
                user.ID_Vendedor = vendedor.ID;
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion", vendedor.Estado.Descripcion);
            return View(vendedor);
        }

        // GET: Vendedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedores.FindAsync(id);
            var user = await _context.Usuarios.FirstOrDefaultAsync(m => m.ID_Vendedor == vendedor.ID);
            vendedor.Usuario.Nombre_Usuario = user.Nombre_Usuario;
            vendedor.Usuario.Contraseña = user.Contraseña;

            if (vendedor == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(vendedor);
        }

        // POST: Vendedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Porc_Comision,ID_Estado,Usuario")] Vendedor vendedor)
        {
            if (id != vendedor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Usuarios.FirstOrDefaultAsync(m => m.ID_Vendedor == vendedor.ID);
                    user.Nombre_Usuario = vendedor.Usuario.Nombre_Usuario;
                    user.Contraseña = vendedor.Usuario.Contraseña;

                    _context.Update(user);
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.ID))
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
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion", vendedor.Estado.Descripcion);
            return View(vendedor);
        }

        // GET: Vendedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedores
                .Include(v => v.Estado)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(vendedor);
        }

        // POST: Vendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(m => m.ID_Vendedor == id);
            _context.Usuarios.Remove(user);

            var vendedor = await _context.Vendedores.FindAsync(id);
            _context.Vendedores.Remove(vendedor);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedores.Any(e => e.ID == id);
        }
    }
}
