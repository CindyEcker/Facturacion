using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Articulo
    {
        public int ID { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Precio Unitario")]
        public decimal Precio_Unitario { get; set; }
        public int Stock { get; set; }
        [ForeignKey("Estado")]
        [Display(Name = "Estado")]
        public int ID_Estado { get; set; }

        public Estado Estado { get; set; }
    }
}
