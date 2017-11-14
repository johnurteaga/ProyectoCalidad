using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Traslado
    {
        public Int32 Id { get; set; }
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public DateTime FechaTraslado { get; set; }

    }
}
