using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class DocenteCurso
    {
        public int Id { get; set; }

        public int DocenteId { get; set; }
        public Docente Docente { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }
        
        public String HoraInicio { get; set; }

        public String HoraFin { get; set; }

        public String Dia { get; set; }

        [NotMapped]
        public int NivelId { get; set; }

        [NotMapped]
        public int GradoId { get; set; }
    }
}
