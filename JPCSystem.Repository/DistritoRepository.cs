using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class DistritoRepository : IDistritoRepository
    {
        private JpcSystemDbContext _context;

        public DistritoRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }

        public IEnumerable<Distrito> GetDistritos()
        {
            var query = from d in _context.Distritos
                select d;

            return query;

        }

        public Distrito GetDistrito(int id)
        {
            return _context.Distritos.Find(id);
        }
    }
}
