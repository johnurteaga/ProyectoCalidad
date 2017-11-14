using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class DocenteService :IDocenteService
    {
        private IDocenteRepository _repository;

        public DocenteService(IDocenteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Docente> GetDocentes(string criterio)
        {
            return _repository.GetDocentes(criterio);
        }

        public Docente GetDocente(int id)
        {
            return _repository.GetDocente(id);
        }

        public void AddDocente(Docente docente)
        {
            _repository.AddDocente(docente);
        }

        public void UpdateDocente(Docente docente)
        {
            _repository.UpdateDocente(docente);
        }

        public void DeleteDocente(int docente)
        {
            _repository.DeleteDocente(docente);
        }
    }
}
