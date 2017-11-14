using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class NivelService : INivelService
    {
        private INivelRepository _repository;

        public NivelService(INivelRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Nivel> GetNiveles()
        {
            return _repository.GetNiveles();
        }

        public Nivel GetNivel(int id)
        {
            return _repository.GetNivel(id);
        }

        public void AddNivel(Nivel nivel)
        {
            _repository.AddNivel(nivel);
        }

        public void UpdateNivel(Nivel nivel)
        {
            _repository.UpdateNivel(nivel);
        }

        public void DeleteNivel(int nivel)
        {
            _repository.DeleteNivel(nivel);
        }
    }
}
