using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Estado
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Vendedor> Vendedores { get; set; }
        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
