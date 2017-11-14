using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class DocenteRepository : IDocenteRepository
    {
        private JpcSystemDbContext _context;

        public DocenteRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }

        public IEnumerable<Docente> GetDocentes(string criterio)
        {
            var query = from d in _context.Docentes.Include("Documento")
                select d;

            if (!string.IsNullOrEmpty(criterio))
            {
                query = from c in query.Include("Documento").Include("Ubigeo")
                    where c.NroDocumento.ToString().ToUpper().Contains(criterio.ToUpper())
                          || (c.Nombres + " " + c.ApPaterno + " " + c.ApMaterno).ToUpper()
                          .Contains(criterio.ToUpper()) || c.ApPaterno.ToUpper().Contains(criterio.ToUpper())
                          || c.ApMaterno.ToUpper().Contains(criterio.ToUpper())
                    select c;
            }
            return query;
        }

        public Docente GetDocente(int id)
        {
            return _context.Docentes.Find(id);
        }

        public void AddDocente(Docente docente)
        {
            _context.Docentes.Add(docente);
            _context.SaveChanges();
        }

        public void UpdateDocente(Docente docente)
        {
            _context.Entry(docente).State=EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDocente(int docente)
        {
            var docentes = _context.Alumnos.Find(docente);

            if (docentes != null)
            {
                _context.Entry(docentes).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}
