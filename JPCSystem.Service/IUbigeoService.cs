using JPCSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Service
{
    public interface IUbigeoService
    {
        IEnumerable<Ubigeo> GetUbigeos();

        Ubigeo GetUbigeo(Int32 id);

        void AddUbigeo(Ubigeo ubigeo);

        void UpdateUbigeo(Ubigeo ubigeo);

        void DeleteUbigeo(Int32 ubigeo);
    }
}
