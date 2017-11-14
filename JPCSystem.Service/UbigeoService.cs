using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class UbigeoService : IUbigeoService
    {

        private IUbigeoRepository _repository;

        //Nueva forma de realizar una conexion con 
        //el Repository aplicando dependencias 
        public UbigeoService(IUbigeoRepository UbigeoRepository)
        {
            _repository = UbigeoRepository;
        }

        public void AddUbigeo(Ubigeo ubigeo)
        {
            throw new NotImplementedException();
        }

        public void DeleteUbigeo(int ubigeo)
        {
            throw new NotImplementedException();
        }

        public Ubigeo GetUbigeo(int id)
        {
            return _repository.GetUbigeo(id);
        }

        public IEnumerable<Ubigeo> GetUbigeos()
        {
            return _repository.GetUbigeos();
        }

        public void UpdateUbigeo(Ubigeo ubigeo)
        {
            _repository.UpdateUbigeo(ubigeo);
        }
    }
}
