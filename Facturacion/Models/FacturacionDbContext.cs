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

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
    }
}
