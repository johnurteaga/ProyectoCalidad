using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Repository
{
    public interface IMasterRepository <T> where T : class
    {
        IEnumerable<T> GetDatos(string criterio);

        T GetDato(Int32 id);

        void AddDato(T dato);

        void UpdateDato(T dato);

        void DeleteDato(Int32 dato);
    }
}
