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
    public class FacturasController : Controller
    {
        private readonly FacturacionDbContext _context;

        public FacturasController(FacturacionDbContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var facturacionDbContext = _context.Facturas.Include(f => f.Cliente).Include(f => f.Vendedor).Include(f => f.Articulo);
            return View(await facturacionDbContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .Include(f => f.Articulo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (factura == null)
            {
                return NotFound();
            }

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "ID", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Vendedor");

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre");
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos, "ID", "Descripcion");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ID_Vendedor,ID_Cliente,Fecha,Comentario,ID_Articulo,Cantidad,Monto")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre");
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos, "ID", "Descripcion");
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre");
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos, "ID", "Descripcion");
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ID_Vendedor,ID_Cliente,Fecha,Comentario,ID_Articulo,Cantidad,Monto")] Factura factura)
        {
            if (id != factura.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.ID))
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

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial", factura.Cliente.Nombre_Comercial);
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre", factura.Vendedor.Nombre);
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos, "ID", "Descripcion", factura.Articulo.Descripcion);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .Include(f => f.Articulo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.ID == id);
        }

        public ActionResult precioUnitario(int id)
        {
            var valorProducto = _context.Articulos.FirstOrDefault(m => m.ID == id);

            return Content(valorProducto.Precio_Unitario.ToString());
        }

        public ActionResult montoTotal(int id, int cantidad)
        {
            var valorProducto = _context.Articulos.FirstOrDefault(m => m.ID == id).Precio_Unitario;
            decimal valor = Decimal.Parse(valorProducto.ToString());

            return Content((valor * cantidad).ToString());
        }
    }
}
