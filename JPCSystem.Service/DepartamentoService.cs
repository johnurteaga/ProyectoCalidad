using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class DepartamentoService : IDepartamentoService
    {
        private IDepartamentoRepository _repository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _repository = departamentoRepository;
        }

        public IEnumerable<Departamento> GetDepartamentos()
        {
            return _repository.GetDepartamentos();
        }

        public Departamento GetDepartamento(int id)
        {
            return _repository.GetDepartamento(id);
        }
    }
}
