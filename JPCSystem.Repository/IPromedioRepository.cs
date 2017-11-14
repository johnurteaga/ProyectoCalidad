using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface IPromedioRepository
    {
        IEnumerable<Promedio> GetPromedios();
        Promedio GetPromedio(int id);

        void AddProvincia(Promedio promedio);

        void UpdaPromedio(Promedio promedio);
    }
}
