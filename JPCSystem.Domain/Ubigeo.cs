using System;
using System.ComponentModel.DataAnnotations;

namespace JPCSystem.Domain
{
    public class Ubigeo
    {
        [StringLength(255)]
        public String Departamento { get; set; }
        [StringLength(255)]
        public String Provincia { get; set; }
        [StringLength(255)]
        public String Distrito { get; set; }

        [Key]
        [StringLength(255)]
        public String IdUbigeo { get; set; }
    }
}
