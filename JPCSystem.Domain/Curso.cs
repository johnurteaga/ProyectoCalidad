using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Curso
    {
        public Int32 Id { get; set; }

        [Required]
        public String NombreCurso { get; set; }

        [NotMapped]
        public int NivelId { get; set; }

        [Required]
        public Int32 GradoId { get; set; }
        public Grado Grado { get; set; }

    }
}
