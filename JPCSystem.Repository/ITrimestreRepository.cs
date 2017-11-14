using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface ITrimestreRepository
    {
        IEnumerable<Trimestre> GeTrimestres();
        Trimestre GeTrimestre(int id);
    }
}
