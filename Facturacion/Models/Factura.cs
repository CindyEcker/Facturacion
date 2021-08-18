using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Factura
    {
        public int ID { get; set; }

        [ForeignKey("Vendedor")]
        [Display(Name = "Vendedor")]
        public int? ID_Vendedor { get; set; }

        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        public int? ID_Cliente { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public string Comentario { get; set; }

        [ForeignKey("Articulo")]
        [Display(Name = "Articulo")]
        public int? ID_Articulo { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Display(Name = "ID Asiento")]
        public int? ID_Asiento { get; set; }

        public Vendedor Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public Articulo Articulo { get; set; }
    }
}
