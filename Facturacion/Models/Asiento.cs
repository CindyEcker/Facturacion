using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Asiento
    {
        public string descripcion { get; set; }
        public int catalogoAuxiliarId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime fecha { get; set; }
        public int monedasId { get; set; }

        public ICollection<Transaccion> transacciones { get; set; }
    }
}
