using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class UbigeoRepository : IUbigeoRepository
    {
        private JpcSystemDbContext _context;

        public UbigeoRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
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
            return _context.Ubigeos.Find(id);
        }

        public IEnumerable<Ubigeo> GetUbigeos()
        {
            var query = from u in _context.Ubigeos
                        select u;

            return query;
        }

        public void UpdateUbigeo(Ubigeo ubigeo)
        {
            _context.Entry(ubigeo).State=EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
