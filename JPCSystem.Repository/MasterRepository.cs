using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Repository
{
    public class MasterRepository<T> : IMasterRepository<T> where T : class
    {
        private JpcSystemDbContext _context;
        private IDbSet<T> _dbSet = null;

        public MasterRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
                _dbSet = _context.Set<T>();
            }

        }

        public void AddDato(T dato)
        {
            _dbSet.Add(dato);
            _context.SaveChanges();
        }

        public void DeleteDato(int dato)
        {
            var alum = _dbSet.Find(dato);

            if (alum != null)
            {
                _context.Entry(alum).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public T GetDato(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetDatos(string criterio)
        {
            var query = from c in _dbSet
                        select c;

            if (!string.IsNullOrEmpty(criterio))
            {
                query = from c in query
                        where c.Equals(criterio.ToUpper())
                        select c;
            }

            return query;
        }

        public void UpdateDato(T dato)
        {
            _context.Entry(dato).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
