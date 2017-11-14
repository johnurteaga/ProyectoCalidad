using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface ICursoRepository
    {
        IEnumerable<Curso> GetCursos(string criterio);

        Curso GetCurso(Int32 id);

        void AddCurso(Curso curso);

        void UpdateCurso(Curso curso);

        void DeleteCurso(Int32 curso);
    }
}
