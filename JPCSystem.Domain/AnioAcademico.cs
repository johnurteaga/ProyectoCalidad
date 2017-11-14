using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class AnioAcademico
    {
        public Int32 Id { get; set; }

        public String Descripcion { get; set; }

        public String Anio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicioMatricula { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinMatricula { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaMatriculaExtemporanea { get; set; }
    }
}
