using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class ApoderadoRepository :IApoderadoRepository
    {

        private JpcSystemDbContext _context;

        public ApoderadoRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext(); ;
            }
        }

        public void AddApoderado(Apoderado apoderado)
        {
            _context.Apoderados.Add(apoderado);
            _context.SaveChanges();
        }

        public void DeleteApoderado(int apoderado)
        {
            var apd= _context.Apoderados.Find(apoderado);

            if (apd != null)
            {
                _context.Entry(apd).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public Apoderado GetApoderado(int id)
        {
            return _context.Apoderados.Find(id);
        }

        public IEnumerable<Apoderado> GetApoderados(string criterio)
        {
            var query = from c in _context.Apoderados.Include("Documento").Include("Ubigeo")
                select c;
            if (!String.IsNullOrEmpty(criterio))
            {
                query = from c in query.Include("Documento").Include("Ubigeo")
                        where c.NroDocumento.ToString().ToUpper().Contains(criterio.ToUpper())
                    select c;
            }
            return query;
        }

        public void UpdateApoderado(Apoderado apoderado)
        {
            _context.Entry(apoderado).State= EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
