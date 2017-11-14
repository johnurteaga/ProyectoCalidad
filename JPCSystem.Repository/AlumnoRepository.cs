using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private JpcSystemDbContext _context;

        public AlumnoRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }

        public void AddAlumno(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            _context.SaveChanges();
        }

        public void DeleteAlumno(int alumno)
        {
            var alum = _context.Alumnos.Find(alumno);

            if (alum != null)
            {
                _context.Alumnos.Remove(alum);
                _context.SaveChanges();
            }
        }

        public Alumno GetAlumno(int id)
        {
            return _context.Alumnos.Find(id);
        }

        public IEnumerable<Alumno> GetAlumnos(string criterio)
        {
            var query = from c in _context.Alumnos.Include("Documento").Include("Ubigeo")
                        select c;

            if (!string.IsNullOrEmpty(criterio))
            {
                query = from c in query.Include("Documento").Include("Ubigeo")
                    where c.NumeroDocumento.ToString().ToUpper().Contains(criterio.ToUpper())
                          || (c.Nombre +" "+ c.ApPaterno+" " + c.ApMaterno).ToUpper().Contains(criterio.ToUpper()) || c.ApPaterno.ToUpper().Contains(criterio.ToUpper())
                          || c.ApMaterno.ToUpper().Contains(criterio.ToUpper())

                    select c;

            }

            return query;
        }

        public void UpdateAlumno(Alumno alumno)
        {
            _context.Entry(alumno).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
