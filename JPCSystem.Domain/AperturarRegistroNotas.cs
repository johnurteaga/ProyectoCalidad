using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Domain
{
    public class AperturarRegistroNotas
    {
        public Int32 Id { get; set; }

        [NotMapped]
        public String nombreAño { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime FechaInicioPrimTrim { get; set; }                              
                                                                                       
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinPrimTrim { get; set; }                               
                                                                                      
        public Int32 AñoAcademicoId { get; set; }
        public AnioAcademico AñoAcademico { get; set; }

        public Boolean estado { get; set; }

        public int TrimestreId { get; set; }
        public Trimestre Trimestre { get; set; }

    }
}
