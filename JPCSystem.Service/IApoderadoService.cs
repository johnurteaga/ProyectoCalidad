using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IApoderadoService
    {
        IEnumerable<Apoderado> GetApoderados(string criterio);

        Apoderado GetApoderado(Int32 id);

        void AddApoderado(Apoderado apoderado);

        void UpdateApoderado(Apoderado apoderado);

        void DeleteApoderado(Int32 apoderado);
    }
}
