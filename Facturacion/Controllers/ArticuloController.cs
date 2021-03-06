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
    public class ArticuloController : Controller
    {
        private readonly FacturacionDbContext _context;

        public ArticuloController(FacturacionDbContext context)
        {
            _context = context;
        }

        // GET: Articulo
        public async Task<IActionResult> Index()
        {
            var facturacionDbContext = _context.Articulos.Include(a => a.Estado);
            return View(await facturacionDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string filter)
        {
            ViewData["filter"] = filter;
            var articulos = _context.Articulos.Include(v => v.Estado).AsQueryable();

            if (!String.IsNullOrEmpty(filter))
            {
                articulos = articulos.Where(x => x.Descripcion.Contains(filter)
                                        || x.Stock.ToString().Contains(filter)
                                        || x.Precio_Unitario.ToString().Contains(filter)
                                        || x.Estado.Descripcion.Contains(filter));
            }

            return View(await articulos.AsNoTracking().ToListAsync());
        }

        // GET: Articulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Estado)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (articulo == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(articulo);
        }

        // GET: Articulo/Create
        public IActionResult Create()
        {
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View();
        }

        // POST: Articulo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Descripcion,Precio_Unitario,Stock,ID_Estado")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion", articulo.Estado.Descripcion);
            return View(articulo);
        }

        // GET: Articulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(articulo);
        }

        // POST: Articulo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Descripcion,Precio_Unitario,Stock,ID_Estado")] Articulo articulo)
        {
            if (id != articulo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.ID))
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

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion", articulo.Estado.Descripcion);
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Estado)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (articulo == null)
            {
                return NotFound();
            }

            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID", "Descripcion");
            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.ID == id);
        }
    }
}
