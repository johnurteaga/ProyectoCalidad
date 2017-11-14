using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPCSystem.Service;

namespace JPCSystem.Web.Models
{
    public class PromedioViewModel
    {
        public int NotaId { get; set; }
        public String DescripcionTrimestre { get; set; }
        public int TrimestreId { get; set; }
        public int AlumnoId { get; set; }
        public String NombreAlumno { get; set; }
        public int NotaT1 { get; set; }
        public int NotaT2 { get; set; }
        public int NotaT3 { get; set; }
        public int PromedioFinal { get; set; }
        public string NotaCuantitativa { get; set; }
    }
}