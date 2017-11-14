using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class TrimestreRepository : ITrimestreRepository
    {
        JpcSystemDbContext _context = new JpcSystemDbContext();

        public IEnumerable<Trimestre> GeTrimestres()
        {
            var query = from t in _context.Trimestres
                select t;

            return query;

        }

        public Trimestre GeTrimestre(int id)
        {
            return _context.Trimestres.Find(id);
        }
    }
}
