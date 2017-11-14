using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using System.Data.Entity;

namespace JPCSystem.Repository
{
    public class NivelRepository : INivelRepository
    {
        private JpcSystemDbContext _context;

        public NivelRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public IEnumerable<Nivel> GetNiveles()
        {
            var query = from c in _context.Niveles
                        select c;
            return query;
        }

        public Nivel GetNivel(int id)
        {
            return _context.Niveles.Find(id);
        }

        public void AddNivel(Nivel nivel)
        {
            _context.Niveles.Add(nivel);
            _context.SaveChanges();
        }

        public void UpdateNivel(Nivel nivel)
        {
            _context.Entry(nivel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteNivel(int nivelId)
        {
            var nivel = _context.Niveles.Find(nivelId);
            if (nivel != null)
            {
                _context.Entry(nivel).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}