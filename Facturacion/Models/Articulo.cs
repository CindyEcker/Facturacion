using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Articulo
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public double Precio_Unitario { get; set; }
        [ForeignKey("Estado")]
        public int ID_Estado { get; set; }

        public Estado Estado { get; set; }
    }
}
