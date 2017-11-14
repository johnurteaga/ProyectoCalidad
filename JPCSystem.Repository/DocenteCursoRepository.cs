using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class DocenteCursoRepository : IDocenteCursoRepository
    {
        private JpcSystemDbContext _context;

        public DocenteCursoRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public IEnumerable<DocenteCurso> GetDocenteCursos(string criterio)
        {
            var query = from dc in _context.DocentesCursos.Include("Curso").Include("Docente").Include("Seccion").Include("Seccion.Grado").Include("Seccion.Grado.Nivel")
                        select dc;

            if (!string.IsNullOrEmpty(criterio))
            {
                query=from dc in _context.DocentesCursos.Include("Curso").Include("Docente").Include("Seccion").Include("Seccion.Grado").Include("Seccion.Grado.Nivel")
                      where dc.Seccion.Grado.NombreGrado.ToUpper().Contains(criterio.ToUpper())
                      select dc;
            }
            return query;
        }

        public DocenteCurso GetDocenteCurso(int id)
        {
            return _context.DocentesCursos.Find(id);
        }

        public void AddDocenteCurso(DocenteCurso docenteCurso)
        {
            _context.DocentesCursos.Add(docenteCurso);
            _context.SaveChanges();
        }

        public void UpdateDocenteCurso(DocenteCurso docenteCurso)
        {
            _context.Entry(docenteCurso).State= EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDocenteCurso(int docenteCurso)
        {
            var dato = _context.DocentesCursos.Find(docenteCurso);

            if (dato!=null)
            {
                _context.Entry(dato).State=EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}
