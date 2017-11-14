using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class PromedioService : IPromedioService
    {
        private IPromedioRepository _repository;

        public PromedioService(IPromedioRepository promedioRepository)
        {
            _repository = promedioRepository;
        }

        public IEnumerable<Promedio> GetPromedios()
        {
            return _repository.GetPromedios();
        }

        public Promedio GetPromedio(int id)
        {
            return _repository.GetPromedio(id);
        }

        public void AddProvincia(Promedio promedio)
        {
            _repository.AddProvincia(promedio);
        }

        public void UpdaPromedio(Promedio promedio)
        {
            _repository.UpdaPromedio(promedio);
        }
    }
}
