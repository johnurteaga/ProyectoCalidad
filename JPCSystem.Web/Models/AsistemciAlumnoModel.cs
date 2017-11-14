using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPCSystem.Web.Models
{
    public class AsistemciAlumnoModel
    {
        public int seccionId { get; set; }
        public String Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public bool Asistencia { get; set; }
        public int AsistenciId { get; set; }
        public int AlumnoId { get; set; }   
        public int AnioAcademicoId { get; set; }
        public string estado { get; set; }

    }
}