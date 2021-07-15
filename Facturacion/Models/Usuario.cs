using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Es requerido")]
        public string Nombre_Usuario { get; set; }
        public string Contraseña { get; set; }
        [ForeignKey("Vendedor")]
        public int ID_Vendedor { get; set; }

        public Vendedor Vendedor { get; set; }
    }
}
