using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Provincia
    {
        public Int32 Id { get; set; }

        [Required]
        public String NombreProvincia { get; set; }

        [Required]
        public Int32 DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
    }
}
