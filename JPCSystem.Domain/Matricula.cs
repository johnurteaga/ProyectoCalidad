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
    public class Matricula
    {
        public int Id { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaMatricula { get; set; }

        public int AnioAcademicoId { get; set; }

        public AnioAcademico AnioAcademico { get; set; }

        public Int32 AlumnoId { get; set; }

        public Alumno Alumno { get; set; }

        public Int32 ApoderadoId { get; set; }

        public Apoderado Apoderado { get; set; }

        [Required]
        public int SeccionId { get; set; }

        public Seccion Seccion { get; set; }

        [NotMapped]
        public string nombreAnio { get; set; }
    }
}