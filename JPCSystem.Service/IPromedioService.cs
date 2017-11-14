using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IPromedioService
    {
        IEnumerable<Promedio> GetPromedios();
        Promedio GetPromedio(int id);

        void AddProvincia(Promedio promedio);

        void UpdaPromedio(Promedio promedio);
    }
}
