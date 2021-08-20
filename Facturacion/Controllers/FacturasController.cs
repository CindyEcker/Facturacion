using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.Models;
using Microsoft.AspNetCore.Http;
using Facturacion.Filters;
using System.Net.Http;
using System.Text;
using System.Net;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace Facturacion.Controllers
{
    [VerificarSession]
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
            var facturacionDbContext = _context.Facturas.Include(f => f.Cliente).Include(f => f.Vendedor)
                                         .Include(f => f.Articulo).Where(v => v.ID_Asiento == null);

            if (facturacionDbContext.Count() != 0)
            {
                var fDesde = _context.Facturas.Min(x => x.Fecha);
                var fHasta = _context.Facturas.Max(x => x.Fecha);
                ViewData["desde"] = fDesde.ToString("yyyy-MM-dd");
                ViewData["hasta"] = fHasta.ToString("yyyy-MM-dd");
            }

            return View(await facturacionDbContext.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Index(string fechaDesde, string fechaHasta)
        {
            var facturas = _context.Facturas.Include(v => v.Vendedor).Include(x => x.Cliente)
                            .Where(v => v.ID_Asiento == null).AsQueryable();

            if(facturas.Count() != 0)
            {
                var fDesde = _context.Facturas.Min(x => x.Fecha);
                var fHasta = _context.Facturas.Max(x => x.Fecha);
                ViewData["desde"] = fDesde.ToString("yyyy-MM-dd");
                ViewData["hasta"] = fHasta.ToString("yyyy-MM-dd");
            }
            
            if (!String.IsNullOrEmpty(fechaDesde) && !String.IsNullOrEmpty(fechaHasta))
            {
                ViewData["desde"] = fechaDesde;
                ViewData["hasta"] = fechaHasta;

                var fd = DateTime.Parse(fechaDesde).Date;
                var fh = DateTime.Parse(fechaHasta).Date;
                facturas = facturas.Where(x => x.Fecha >= fd && x.Fecha <= fh);
            }

            return View(await facturas.AsNoTracking().ToListAsync());
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
        public async Task<IActionResult> Create()
        {
            string session = HttpContext.Session.GetString("Usuario");
            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.Usuario.Nombre_Usuario == session);

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre", vendedor.ID);
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos.Where(a => a.Stock != 0), "ID", "Descripcion");
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
                var art = await _context.Articulos.FindAsync(factura.ID_Articulo);

                if (factura.Cantidad <= art.Stock)
                {
                    _context.Add(factura);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Error"] = $"No hay suficiente de este articulo. Quedan {art.Stock} en stock.";
                }
            }

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre");
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos.Where(a => a.Stock != 0), "ID", "Descripcion");
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
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos.Where(a => a.Stock != 0), "ID", "Descripcion");
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
                var art = await _context.Articulos.FindAsync(factura.ID_Articulo);

                if (factura.Cantidad <= art.Stock)
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
                else
                {
                    ViewData["Error"] = $"No hay suficiente de este articulo. Quedan {art.Stock} en stock.";
                }
            }

            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Nombre_Comercial");
            ViewData["ID_Vendedor"] = new SelectList(_context.Vendedores, "ID", "Nombre");
            ViewData["ID_Articulo"] = new SelectList(_context.Articulos.Where(a => a.Stock != 0), "ID", "Descripcion");
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

        public ActionResult detallesArticulo(int id)
        {
            var articulo = _context.Articulos.FirstOrDefault(m => m.ID == id);
            string precio = articulo.Precio_Unitario.ToString();

            return Content(precio);
        }

        public ActionResult montoTotal(int id, int cantidad)
        {
            var valorProducto = _context.Articulos.FirstOrDefault(m => m.ID == id).Precio_Unitario;
            decimal valor = Decimal.Parse(valorProducto.ToString());

            return Content((valor * cantidad).ToString());
        }

        public async Task<ActionResult> ContabilizarAsync(string fechaDesde, string fechaHasta)
        {
            ViewData["desde"] = fechaDesde;
            ViewData["hasta"] = fechaHasta;

            if (!String.IsNullOrEmpty(fechaDesde) && !String.IsNullOrEmpty(fechaHasta))
            {
                var fd = DateTime.Parse(fechaDesde).Date;
                var fh = DateTime.Parse(fechaHasta).Date;

                var facturas = _context.Facturas.Include(v => v.Vendedor).Include(x => x.Cliente)
                                .Where(v => v.ID_Asiento == null).Where(x => x.Fecha >= fd && x.Fecha <= fh);

                decimal monto = facturas.Sum(x => x.Monto);
                var periodo = DateTime.Parse(fechaDesde).ToString("yyyy-MM");

                var asiento = new Asiento
                {
                    descripcion = $"Asiento de Facturacion correspondiente al periodo {periodo}",
                    catalogoAuxiliarId = 3,
                    fecha = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    monedasId = 1,
                    transacciones = new List<Transaccion>
                        {
                            new Transaccion
                            {
                                cuentasContablesId = 13,
                                tipoMovimientoId = 1,
                                monto = monto
                            },

                            new Transaccion
                            {
                                cuentasContablesId = 6,
                                tipoMovimientoId = 2,
                                monto = monto
                            }
                        }
                };

                var url = "https://contabilidad2021.azurewebsites.net/api/Asientos";
                var json = JsonConvert.SerializeObject(asiento);
                HttpClient httpClient = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    dynamic jsonObj = JsonConvert.DeserializeObject(contents);
                    var numAsiento = jsonObj["id"].ToString();

                    var facts = facturas.ToList();
                    foreach (Factura f in facturas)
                    {
                        f.ID_Asiento = Int32.Parse(numAsiento);
                    }

                    _context.SaveChanges();
                }
                else
                {
                    ViewData["Error"] = "No se pudo contabilizar";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
