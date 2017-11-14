using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Promedio
    {
        public Int32 Id { get; set; }

        [Required]
        public Int32 PrimerTrimPromedio { get; set; }

        [NotMapped]
        public Int32 SegundoTrimPromedio { get; set; }

        [NotMapped]
        public Int32 TercerTrimPromedio { get; set; }

        [NotMapped]
        public int PromedioCurso { get; set; }

        [Required]
        public int PromedioFinal { get; set; }

        [Required]
        public Int32 NotaId { get; set; }
        public Nota Nota { get; set; }
    }
}
