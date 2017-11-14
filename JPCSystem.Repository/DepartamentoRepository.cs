using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class DepartamentoRepository :IDepartamentoRepository
    {
        private JpcSystemDbContext _context;
        public DepartamentoRepository()
        {
            if (_context==null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public IEnumerable<Departamento> GetDepartamentos()
        {
            var query = from departamento in _context.Departamentos
                        select departamento;

            return query;
        }

        public Departamento GetDepartamento(int id)
        {
            return _context.Departamentos.Find(id);
        }
    }
}
