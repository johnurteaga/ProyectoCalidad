using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Documento
    {
        public Int32 Id { get; set; }

        [Required]
        public String NomDocumento { get; set; }
    }
}
