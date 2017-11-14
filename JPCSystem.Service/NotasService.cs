using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class NotasService : INotasService
    {
        private INotasRepository _repository;

        public NotasService(INotasRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Nota> GetNotas(string criterio)
        {
            return _repository.GetNotas(criterio);
        }

        public Nota GetNota(int id)
        {
            return _repository.GetNota(id);
        }

        public void AddNota(Nota nota)
        {
            _repository.AddNota(nota);
        }

        public void UpdateNota(Nota nota)
        {
            _repository.UpdateNota(nota);
        }

        public void DeleteNota(int nota)
        {
            _repository.DeleteNota(nota);
        }
    }
}
