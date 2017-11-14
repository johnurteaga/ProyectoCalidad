using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class ApoderadoService : IApoderadoService
    {
        private IApoderadoRepository _repository;

        //public ApoderadoService()
        //{
        //    if (_repository == null)
        //    {
        //        _repository = new ApoderadoRepository();
        //    }
        //}

        public ApoderadoService(IApoderadoRepository repository)
        {
            _repository = repository;
        }

        public void AddApoderado(Apoderado apoderado)
        {
            _repository.AddApoderado(apoderado);
        }

        public void DeleteApoderado(int apoderado)
        {
            _repository.DeleteApoderado(apoderado);
        }

        public Apoderado GetApoderado(int id)
        {
            return _repository.GetApoderado(id);
        }

        public IEnumerable<Apoderado> GetApoderados(string criterio)
        {
            return _repository.GetApoderados(criterio);
        }

        public void UpdateApoderado(Apoderado apoderado)
        {
            _repository.UpdateApoderado(apoderado);
        }
    }
}
