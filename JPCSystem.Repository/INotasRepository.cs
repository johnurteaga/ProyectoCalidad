using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface INotasRepository
    {
        IEnumerable<Nota> GetNotas(string criterio);

        Nota GetNota(Int32 id);

        void AddNota(Nota nota);

        void UpdateNota(Nota nota);

        void DeleteNota(Int32 nota);
    }
}
