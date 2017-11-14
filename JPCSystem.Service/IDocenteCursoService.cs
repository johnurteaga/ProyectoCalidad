using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IDocenteCursoService
    {
        IEnumerable<DocenteCurso> GetDocenteCursos(string criterio);
        DocenteCurso GetDocenteCurso(int id);
        void AddDocenteCurso(DocenteCurso docenteCurso);
        void UpdateDocenteCurso(DocenteCurso docenteCurso);
        void DeleteDocenteCurso(int docenteCurso);
    }
}
