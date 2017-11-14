using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPCSystem.Web.Models
{
    public class MatriculaModel
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int ApoderadoId { get; set; }
        public int AnioAcademicoId { get; set; }
        public int tipoDocAlumnoId { get; set; }
        public string NombreAlumno { get; set; }
        public int nroDocAlumno { get; set; }
        public int tipoDocApoderadoId { get; set; }
        public int nroDocApoderado { get; set; }
        public string Nombreapoderado { get; set; }
        public int niveId { get; set; }
        public int gradoId { get; set; }
        public int seccinId { get; set; }
        public int vacantes { get; set; }
        public int matriculados { get; set; }
        public DateTime FechaDateTime { get; set; }
        public string anio { get; set; }
    }
}