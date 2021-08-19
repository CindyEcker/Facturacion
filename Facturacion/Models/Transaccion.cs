using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Models
{
    public class Transaccion
    {
        public int cuentasContablesId { get; set; }
        public int tipoMovimientoId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal monto { get; set; }
    }
}
