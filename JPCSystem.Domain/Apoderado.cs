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
    public class Apoderado
    {
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Int32 NroDocumento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String ApPaterno { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String ApMaterno { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Direccion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.EmailAddress)]
        public String Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Genero { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Int32 Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String LugarTrabajo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Int32 DocumentoId { get; set; }

        public Documento Documento { get; set; }

        public String IdUbigeo { get; set; }
        public Ubigeo Ubigeo { get; set; }

        [NotMapped]
        public Int32 Edad => DateTime.Now.Year - FechaNacimiento.Year;

        [NotMapped]
        public String NombreCompleto => Nombres + " " + ApPaterno + " " + ApMaterno;
    }
}
    