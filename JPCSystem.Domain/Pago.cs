using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Pago
    {
        public Int32 Id { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        public Int32 Monto { get; set; }

        public String Descripccion { get; set; }
    }
}
