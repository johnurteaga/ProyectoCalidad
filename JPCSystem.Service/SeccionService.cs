using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class SeccionService : ISeccionService
    {
        private ISeccionRepository _repository;

        public SeccionService(ISeccionRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Seccion> GetSecciones(string criterio)
        {
            return _repository.GetSecciones(criterio);
        }

        public Seccion GetSeccion(int id)
        {
            return _repository.GetSeccion(id);
        }

        public void AddSeccion(Seccion seccion)
        {
            _repository.AddSeccion(seccion);
        }

        public void UpdateSeccion(Seccion seccion)
        {
           _repository.UpdateSeccion(seccion);
        }

        public void DeleteSeccion(int seccion)
        {
            _repository.DeleteSeccion(seccion);
        }

        public IQueryable<Seccion> GetSeccionesQueryable()
        {
            return _repository.GetSeccionesQueryable();
        }
    }
}
