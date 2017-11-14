using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class Docente
    {
        public Int32 Id { get; set; }

        [Required]
        public Int32 NroDocumento { get; set; }

        [Required]
        public String ApPaterno { get; set; }

        [Required]
        public String ApMaterno { get; set; }

        [Required]
        public String Nombres { get; set; }

        [Required]
        public String Direccion { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public String Genero { get; set; }

        [Required]
        public Int32 Telefono { get; set; }

        [Required]
        public Int32 AniosExperiencia { get; set; }

        [Required]
        public String EstadoCivil { get; set; }

        [Required]
        public Int32 DocumentoId { get; set; }
        public Documento Documento { get; set; }

        public String IdUbigeo { get; set; }
        public Ubigeo Ubigeo { get; set; }

        [NotMapped]
        public String NombreCompleto => Nombres +" "+ApPaterno +" "+ApMaterno;
    }
}
