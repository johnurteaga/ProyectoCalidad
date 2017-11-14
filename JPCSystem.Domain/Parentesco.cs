using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Parentesco
    {
        public Int32 Id { get; set; }
        public String TipoParentesco { get; set; }
        public Int32? AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public Int32? ApoderadoId { get; set; }
        public Apoderado Apoderado { get; set; }
    }
}
