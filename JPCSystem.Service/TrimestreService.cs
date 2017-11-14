using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class TrimestreService : ITrimestreService
    {
        private ITrimestreRepository _repository;

        public TrimestreService(ITrimestreRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Trimestre> GeTrimestres()
        {
            return _repository.GeTrimestres();
        }

        public Trimestre GeTrimestre(int id)
        {
            return _repository.GeTrimestre(id);
        }
    }
}
