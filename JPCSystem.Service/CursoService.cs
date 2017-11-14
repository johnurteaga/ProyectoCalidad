using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class CursoService : ICursoService
    {
        private ICursoRepository _repository;

        public CursoService(ICursoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Curso> GetCursos(string criterio)
        {
            return _repository.GetCursos(criterio);
        }

        public Curso GetCurso(int id)
        {
            return _repository.GetCurso(id);
        }

        public void AddCurso(Curso curso)
        {
            _repository.AddCurso(curso);
        }

        public void UpdateCurso(Curso curso)
        {
            _repository.UpdateCurso(curso);
        }

        public void DeleteCurso(int curso)
        {
            _repository.DeleteCurso(curso);
        }
    }
}
