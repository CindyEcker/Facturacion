using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Models
{
    public class FacturacionDbContext : DbContext
    {
        public FacturacionDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Estado> Estados { get; set; }
        DbSet<Articulo> Articulos { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Vendedor> Vendedores { get; set; }
    }
}
