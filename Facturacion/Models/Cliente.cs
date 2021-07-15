using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Display(Name ="Nombre Comercial")]
        public string Nombre_Comercial { get; set; }

        public int RNC { get; set; }

        public string Direccion { get; set; }

        public int Cuenta_Contable { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public int ID_Estado { get; set; }
        public Estado Estado { get; set; }








    }
}
