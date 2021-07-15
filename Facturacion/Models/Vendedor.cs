using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
=======
>>>>>>> fcbf2418a77cb95776e29517201e1c737e327c17
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Vendedor
    {

        [Required(ErrorMessage = "Es requerido")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Porcentaje de comisión")]
        [Required(ErrorMessage = "Es requerido")]
        public int? Porc_Comision { get; set; }

        [ForeignKey("Estado")]
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Es requerido")]
        public int? ID_Estado { get; set; }

        public Estado Estado { get; set; }
        public Usuario Usuario { get; set; }
    }
}
