using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Departamento
    {
        public Int32 Id { get; set; }
        [Required]
        public String NombreDepartamento { get; set; }
    }
}
