using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class DistritoService : IDistritoService
    {
        private IDistritoRepository _repository;

        public DistritoService(IDistritoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Distrito> GetDistritos()
        {
            return _repository.GetDistritos();
        }

        public Distrito GetDistrito(int id)
        {
            return _repository.GetDistrito(id);
        }
    }
}
