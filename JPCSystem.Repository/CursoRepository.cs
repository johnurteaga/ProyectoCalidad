using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class CursoRepository : ICursoRepository
    {
        private JpcSystemDbContext _context;

        public CursoRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }


        public IEnumerable<Curso> GetCursos(string criterio)
        {
            var query = from c in _context.Cursos.Include("Grado").Include("Grado.Nivel")
                select c;

            if (!String.IsNullOrEmpty(criterio))
            {
                query = from c in query.Include("Grado").Include("Grado.Nivel")
                        where c.NombreCurso.ToUpper().Contains(criterio.ToUpper())
                    select c;

            }
            return query;
        }

        public Curso GetCurso(int id)
        {
            return _context.Cursos.Find(id);
        }

        public void AddCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            _context.SaveChanges();
        }

        public void UpdateCurso(Curso curso)
        {
            _context.Entry(curso).State=EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCurso(int curso)
        {
            var cur = _context.Cursos.Find(curso);

            if (cur!=null)
            {
                _context.Cursos.Remove(cur);
                _context.SaveChanges();
            }
        }
    }
}
