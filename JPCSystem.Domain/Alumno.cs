using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JPCSystem.Domain
{
    public class Alumno
    {
        [Key]
        public int Id { get; set; }

       
        public int NumeroDocumento { get; set; }

        public String ApPaterno { get; set; }

        public String ApMaterno { get; set; }

        public String Nombre { get; set; }

        public String IdUbigeo { get; set; }
        public Ubigeo Ubigeo { get; set; }

        public String Direccion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.EmailAddress)]
        public String Correo { get; set; }

        public String Genero { get; set; }

        public int Telefono { get; set; }

        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }

        public Boolean Estado { get; set; }

        [NotMapped]
        public Int32 Edad {
            get
            {
                return DateTime.Now.Year - FechaNacimiento.Year;
            }
        }

        [NotMapped]
        public String NombreCompleto
        {
            get
            {
                return Nombre + " " + ApPaterno + " " + ApMaterno;
            }
        }

    }
}
