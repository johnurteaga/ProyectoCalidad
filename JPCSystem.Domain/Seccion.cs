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
    public class Seccion
    {
        public int Id { get; set; }

        public String NombreSeccion { get; set; }

        public int GradoId { get; set; }

        public Grado Grado { get; set; }

        [NotMapped]
        public int Alumnos { get; set; }

        public int? Capasida { get; set; }

        [NotMapped]
        public int NivelId { get; set; }

    }
}