using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JPCSystem.Domain;

namespace JPCSystem.Web.Models
{
    public class NotasViewModel
    {
        [Required]
        public Int32 TrimestreId { get; set; }

        [Required]
        public Int32 CursoId { get; set; }

        [Required]
        public int GradoId { get; set; }

        [Required]
        public int SeccionId { get; set; }

        [Required]
        public int NivelId { get; set; }

        public List<AlumnosModel> Alumnos { get; set; }

    }

    public class AlumnosModel
    {
        public Int32 IdAlumno { get; set; }

        public String Alumno { get; set; }

        public String Cualitativa { get; set; }

        public Int32 Vigecimal { get; set; }

        public Int32 Nota1 { get; set; }

        public Int32 Nota2 { get; set; }

        public Int32 Nota3 { get; set; }

        public Int32 Nota4 { get; set; }

    }
}