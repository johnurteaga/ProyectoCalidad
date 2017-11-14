using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private JpcSystemDbContext _context;

        public AsistenciaRepository()
        {
            if (_context==null)
            {
                _context = new JpcSystemDbContext();
            }
        }


        public IEnumerable<Asistencia> GetAsistencias(string criterio )
        {
            var query = from a in _context.Asistencias.Include("Seccion.Grado").
                    Include("Seccion.Grado.Nivel").Include("Alumno")
                        select a;

            if (!string.IsNullOrEmpty(criterio))
            {
                var actual = DateTime.Now.Year.ToString();
                query = from a in query.Include("Seccion").Include("Seccion.Grado").
                        Include("Seccion.Grado.Nivel").Include("Alumno")
                        where (a.Alumno.Nombre.ToUpper() + " " + a.Alumno.ApPaterno.ToUpper()
                            + " " + a.Alumno.ApMaterno).Contains(criterio.ToUpper()) ||
                            (a.Alumno.ApPaterno.ToUpper() + " " + a.Alumno.ApMaterno + " " +
                            a.Alumno.Nombre.ToUpper()).Contains(criterio.ToUpper()) ||
                            a.Alumno.Nombre.ToUpper().Contains(criterio.ToUpper()) ||
                             a.Alumno.ApPaterno.ToUpper().Contains(criterio.ToUpper()) ||
                              a.Alumno.ApMaterno.ToUpper().Contains(criterio.ToUpper())
                        select a;
            }
            return query;
        }

        public Asistencia GetAsistencia(int id)
        {
            return _context.Asistencias.Find(id);
        }

        public void AddAsistencia(Asistencia asistencia)
        {
            _context.Asistencias.Add(asistencia);
            _context.SaveChanges();
        }

        public void UpdateAsistencia(Asistencia asistencia)
        {
            _context.Entry(asistencia).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
