using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class PromedioRepository : IPromedioRepository
    {
        private JpcSystemDbContext _context;

        public PromedioRepository()
        {
            if (_context== null)
            {
                _context= new JpcSystemDbContext();;
            }
        }

        public IEnumerable<Promedio> GetPromedios()
        {
            var query = from p in _context.Promedios
                select p;

            return query;

        }

        public Promedio GetPromedio(int id)
        {
            return _context.Promedios.Find(id);
        }

        public void AddProvincia(Promedio promedio)
        {
            _context.Promedios.Add(promedio);
            _context.SaveChanges();
        }

        public void UpdaPromedio(Promedio promedio)
        {
            _context.Entry(promedio).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
