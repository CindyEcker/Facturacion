using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Vendedor
    {

        public int ID { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Porcentaje de comisión")]
        public int? Porc_Comision { get; set; }

        [ForeignKey("Estado")]
        [Display(Name = "Estado")]
        public int? ID_Estado { get; set; }

        public Estado Estado { get; set; }
        public Usuario Usuario { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
