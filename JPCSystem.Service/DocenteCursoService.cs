using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class DocenteCursoService : IDocenteCursoService
    {
        private IDocenteCursoRepository _repository;

        public DocenteCursoService(IDocenteCursoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DocenteCurso> GetDocenteCursos(string criterio)
        {
            return _repository.GetDocenteCursos(criterio);
        }

        public DocenteCurso GetDocenteCurso(int id)
        {
            return _repository.GetDocenteCurso(id);
        }

        public void AddDocenteCurso(DocenteCurso docenteCurso)
        {
            _repository.AddDocenteCurso(docenteCurso);
        }

        public void UpdateDocenteCurso(DocenteCurso docenteCurso)
        {
            _repository.UpdateDocenteCurso(docenteCurso);
        }

        public void DeleteDocenteCurso(int docenteCurso)
        {
            _repository.DeleteDocenteCurso(docenteCurso);
        }
    }
}
