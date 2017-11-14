using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Grado
    {
        public int Id { get; set; }

        [Required]
        public String NombreGrado { get; set; }

        [NotMapped]
        public int numAlumnos { get; set; }

        [Required]
        public int NivelId { get; set; }

        public Nivel Nivel { get; set; }
    }
}