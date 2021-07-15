using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Estado
    {
        public int ID { get; set; }
        [Display(Name = "Descripción")]

        public string Descripcion { get; set; }

        public virtual ICollection<Vendedor> Vendedores { get; set; }
        public virtual ICollection<Articulo> Articulos { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
