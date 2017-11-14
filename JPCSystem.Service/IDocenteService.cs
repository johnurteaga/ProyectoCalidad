using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IDocenteService
    {
        IEnumerable<Docente> GetDocentes(string criterio);
        Docente GetDocente(Int32 id);
        void AddDocente(Docente docente);
        void UpdateDocente(Docente docente);
        void DeleteDocente(Int32 docente);
    }
}
