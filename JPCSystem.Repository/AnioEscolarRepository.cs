using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class AnioEscolarRepository :IAnioEscolarRepository
    {
        private JpcSystemDbContext _context;

        public AnioEscolarRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }


        public IEnumerable<AnioAcademico> GetAnioAcademicos(String criterio)
        {
            var query = from c in _context.AnioAcademicos
                select c;
            if (!string.IsNullOrEmpty(criterio))
            {
                query = from c in _context.AnioAcademicos
                        where c.Anio.ToUpper().Contains(criterio.ToUpper())
                        select c;
            }

            return query;
        }

        public AnioAcademico GetAnioAcademico(int id)
        {
            return _context.AnioAcademicos.Find(id);
        }

        public void AddAnioAcademico(AnioAcademico anioAcademico)
        {
            _context.AnioAcademicos.Add(anioAcademico);
            _context.SaveChanges();
        }

        public void UpdateAnioAcademico(AnioAcademico anioAcademico)
        {
            _context.Entry(anioAcademico).State=EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteAnioAcademico(int anioAcademico)
        {
            var anio = _context.Alumnos.Find(anioAcademico);

            if (anio != null)
            {
                _context.Entry(anio).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}
