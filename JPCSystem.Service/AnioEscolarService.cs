using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class AnioEscolarService : IAnioEscolarService
    {
        private IAnioEscolarRepository _repository;

        //public AnioEscolarService()
        //{
        //    if (_repository==null)
        //    {
        //        _repository=new AñoEscolarRepository();
        //    }
        //}

        public AnioEscolarService(IAnioEscolarRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AnioAcademico> GetAniosAcademicos(string criterio)
        {
            return _repository.GetAnioAcademicos(criterio);
        }

        public AnioAcademico GetAnioAcademico(int id)
        {
            return _repository.GetAnioAcademico(id);
        }

        public void AddAnioAcademico(AnioAcademico anioAcademico)
        {
            _repository.AddAnioAcademico(anioAcademico);
        }

        public void UpdateAnioAcademico(AnioAcademico anioAcademico)
        {
            _repository.UpdateAnioAcademico(anioAcademico);
        }

        public void DeleteAnioAcademico(int anioAcademico)
        {
            _repository.DeleteAnioAcademico(anioAcademico);
        }
    }
}
