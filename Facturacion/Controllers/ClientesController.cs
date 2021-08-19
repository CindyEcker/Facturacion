using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.Models;

namespace Facturacion.Controllers
{
    public class ClientesController : Controller
    {
        private readonly FacturacionDbContext _context;

        public ClientesController(FacturacionDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var facturacionDbContext = _context.Clientes.Include(v => v.Estado);
            return View(await facturacionDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string filter)
        {
            ViewData["filter"] = filter;
            var clientes = _context.Clientes.Include(v => v.Estado).AsQueryable();

            if (!String.IsNullOrEmpty(filter))
            {
                clientes = clientes.Where(x => x.Nombre_Comercial.Contains(filter)
                                || x.RNC.ToString().Contains(filter)
                                || x.Cuenta_Contable.ToString().Contains(filter)
                                || x.Telefono.Contains(filter)
                                || x.Email.Contains(filter)
                                || x.Estado.Descripcion.Contains(filter));
            }

            return View(await clientes.AsNoTracking().ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre_Comercial,RNC,Direccion,Cuenta_Contable,Telefono,Email,ID_Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (esUnRNCValido(cliente.RNC.ToString()))
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Error"] = "El RNC ingresado no es valido.";
                }
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre_Comercial,RNC,Direccion,Cuenta_Contable,Telefono,Email,ID_Estado")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion", cliente.Estado.Descripcion);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void AgregarEstadosAViewBag()
        {
            ViewBag.Estados = _context.Estados;
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        private bool esUnRNCValido(string pRNC)
        {
            try
            {
                int vnTotal = 0;
                int[] digitoMult = new int[8] { 7, 9, 8, 6, 5, 4, 3, 2 };
                string vcRNC = pRNC.Replace("-", "").Replace(" ", "");
                string vDigito = vcRNC.Substring(8, 1);

                if (vcRNC.Length.Equals(9))
                {
                    for (int vDig = 1; vDig <= 8; vDig++)
                    {
                        int vCalculo = Int32.Parse(vcRNC.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                        vnTotal += vCalculo;
                    }

                    if (vnTotal % 11 == 0 && vDigito == "1" || vnTotal % 11 == 1 && vDigito == "1" || (11 - (vnTotal % 11)) == Int32.Parse(vDigito))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
