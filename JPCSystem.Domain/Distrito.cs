using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Distrito
    {
        public Int32 Id { get; set; }

        [Required]
        public String NombreDistrito { get; set; }

        [Required]
        public Int32 ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
    }
}
