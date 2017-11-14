using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace JPCSystem.Repository
{
    public class GradoRepository : IGradoRepository
    {
        private JpcSystemDbContext _context;

        public GradoRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public IEnumerable<Grado> GetGrados()
        {
            var query = from g in _context.Grados.Include("Nivel")
                        select g;

            return query;
        }

        public Grado GetGrado(int id)
        {
            return _context.Grados.Find(id);
        }

        public void AddGrado(Grado grado)
        {
            _context.Grados.Add(grado);
            _context.SaveChanges();
        }

        public void UpdateGrado(Grado grado)
        {
            _context.Entry(grado).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteGrado(int grado)
        {
            var g = _context.Grados.Find(grado);
            if (g != null)
            {
                _context.Entry(g).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}