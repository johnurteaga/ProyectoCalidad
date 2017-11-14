using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class SeccionRepository : ISeccionRepository
    {
        private JpcSystemDbContext _context;

        public SeccionRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public IEnumerable<Seccion> GetSecciones(string criterio)
        {
            var query = from s in _context.Secciones.Include("Grado").Include("Grado.Nivel")
                        select s;

            return query;
        }

        public IQueryable<Seccion> GetSeccionesQueryable()
        {
            var query = from s in _context.Secciones.Include("Grado").Include("Grado.Nivel")
                select s;

            return query.OrderBy(s=>s.NombreSeccion);
        }

        public Seccion GetSeccion(int id)
        {
            return _context.Secciones.Find(id);
        }

        public void AddSeccion(Seccion seccion)
        {
            _context.Secciones.Add(seccion);
            _context.SaveChanges();
        }

        public void UpdateSeccion(Seccion seccion)
        {
            _context.Entry(seccion).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSeccion(int seccion)
        {
            var secc = _context.Secciones.Find(seccion);
            if (secc != null)
            {
                _context.Entry(secc).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}