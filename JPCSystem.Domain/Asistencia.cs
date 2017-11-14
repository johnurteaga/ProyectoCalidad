using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Asistencia
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAsistencia { get; set; }

        [Required]
        public Int32 SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        [Required]
        public Int32 AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [Required]
        public int AnioAcademicoId { get; set; }
        public AnioAcademico AnioAcademico { get; set; }
    }
}
