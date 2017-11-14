using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class RegNotasService:IRegNotasService
    {
        private IRegNotasRepository _repository;

        public RegNotasService(IRegNotasRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<AperturarRegistroNotas> GetRegistrosNotas(string criterio)
        {
            return _repository.GetRegistrosNotas(criterio);
        }

        public AperturarRegistroNotas GetRegistroNotas(int id)
        {
            return _repository.GetRegistroNotas(id);
        }

        public void AddRegistroNotas(AperturarRegistroNotas aperturarRegistro)
        {
            _repository.AddRegistroNotas(aperturarRegistro);
        }

        public void UpdateRegistroNotas(AperturarRegistroNotas aperturarRegistro)
        {
            _repository.UpdateRegistroNotas(aperturarRegistro);
        }

        public void DeleteRegistroNotas(int registroNotas)
        {
            _repository.DeleteRegistroNotas(registroNotas);
        }
    }
}
