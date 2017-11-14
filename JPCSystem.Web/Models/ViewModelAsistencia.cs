using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JPCSystem.Domain;

namespace JPCSystem.Web.Models
{
    public class ViewModelAsistencia
    {
        [Required]
        public DateTime FechaAsistencia { get; set; }

        [Required]
        public Int32 GradoId { get; set; }


        [Required]
        public Int32 SeccionId { get; set; }


        [Required]
        public Int32 NivelId { get; set; }


        [Required]
        public int CursoId { get; set; }


        public List<AlumnoModelAsiste> Alumnos { get; set; } 
    }

    public class AlumnoModelAsiste
    {
        public Int32 IdAlumno { get; set; }
        public string Alumno { get; set; }
        public bool Asistio { get; set; }
        public String Asis{ get; set; }
    }
}