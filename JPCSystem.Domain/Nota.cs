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
    public class Nota
    {
        public Int32 Id { get; set; }

        [Required]
        public String Cualitativa { get; set; }

        [Required]
        public Int32 Vigecimal { get; set; }

        [Required]
        public Int32 Nota1 { get; set; }

        [Required]
        public Int32 Nota2 { get; set; }

        [Required]
        public Int32 Nota3 { get; set; }

        [Required]
        public Int32 Nota4 { get; set; }

        [Required]
        public Int32 TrimestreId { get; set; }
        public Trimestre Trimestre { get; set; }

        [Required]
        public Int32 CursoId { get; set; }
        public Curso Curso { get; set; }

        [Required]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [Required]
        public int SeccionId { get; set; }
        public Seccion Seccion{ get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaRegistro { get; set; }

        [NotMapped]
        public int GradoId { get; set; }

        [NotMapped]
        public int NivelId { get; set; }
    }
}
