using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IRegNotasService
    {
        IEnumerable<AperturarRegistroNotas> GetRegistrosNotas(String criterio);
        AperturarRegistroNotas GetRegistroNotas(Int32 id);
        void AddRegistroNotas(AperturarRegistroNotas aperturarRegistro);
        void UpdateRegistroNotas(AperturarRegistroNotas aperturarRegistro);
        void DeleteRegistroNotas(int registroNotas);
    }
}
