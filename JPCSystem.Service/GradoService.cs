using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class GradoService :IGradoService
    {
        private IGradoRepository _repository;

        public GradoService(IGradoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Grado> GetGrados()
        {
           return _repository.GetGrados();
        }

        public Grado GetGrado(int id)
        {
            return _repository.GetGrado(id);
        }

        public void AddGrado(Grado grado)
        {
            _repository.AddGrado(grado);
        }

        public void UpdateGrado(Grado grado)
        {
            _repository.UpdateGrado(grado);
        }

        public void DeleteGrado(int grado)
        {
            throw new NotImplementedException();
        }
    }
}
