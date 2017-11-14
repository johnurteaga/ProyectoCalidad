using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Usuario
    {
        public int id { get; set; }
        public int? DocenteId { get; set; }
        public int? ApoderadoId { get; set; }
        public int? AlumnoId { get; set; }
    }
}
