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
        [Required(ErrorMessage = "Es requerido")]
        public string Descripcion { get; set; }
        [Display(Name = "Precio Unitario")]
        [Required(ErrorMessage = "Es requerido")]
        public decimal Precio_Unitario { get; set; }
        [ForeignKey("Estado")]
        [Required(ErrorMessage = "Es requerido")]
        public int ID_Estado { get; set; }

        public Estado Estado { get; set; }
    }
}
