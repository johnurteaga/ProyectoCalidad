using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private JpcSystemDbContext _context;

        public MatriculaRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public IEnumerable<Matricula> GetMatriculas(string criterio)
        {
            var query = from c in _context.Matriculas.Include("Alumno").Include("Apoderado")
                        .Include("Seccion").Include("Seccion.Grado").Include("Seccion.Grado.Nivel").Include("AnioAcademico")
                        select c;

            if (!string.IsNullOrEmpty(criterio))
            {
                query = from c in query.Include("Alumno").Include("Apoderado")
                        .Include("Seccion").Include("Seccion.Grado").Include("Seccion.Grado.Nivel").Include("AnioAcademico")
                        where c.Alumno.Nombre.ToUpper().Contains(criterio.ToUpper()) || c.Alumno.ApPaterno.ToUpper().Contains(criterio.ToUpper()) ||
                              c.Alumno.ApMaterno.ToUpper().Contains(criterio) || 
                              (c.Alumno.Nombre + " " + c.Alumno.ApPaterno +" " + c.Alumno.ApMaterno).ToUpper().Contains(criterio.ToUpper())
                        select c;
            }

            return query;
        }

        public Matricula GetMatricula(int id)
        {
            return _context.Matriculas.Find(id);
        }

        public void AddMatricula(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            _context.SaveChanges();
        }

        public void UpdateMatricula(Matricula matricula)
        {
            _context.Entry(matricula).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMatricula(int matricula)
        {
            var mat = _context.Matriculas.Find(matricula);

            if (mat != null)
            {
                _context.Matriculas.Remove(mat);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Alumno> GetAlumnos(int seccionId)
        {
            var actual = DateTime.Now.Year;
            var matriculados = from a in _context.Matriculas.Include("AnioAcademico")
                               where a.SeccionId.Equals(seccionId) && a.AnioAcademico.Anio.Equals(actual.ToString())
                               select a.AlumnoId;

            var alumnos = from a in _context.Alumnos
                          where matriculados.Contains(a.Id)
                          select a;

            return alumnos;
        }

        public IEnumerable<Alumno> GetAlumnosNotas(int seccionId, int cursoId)
        {
            var matriculados = from a in _context.Matriculas
                               where a.SeccionId.Equals(seccionId)
                               select a.AlumnoId;

            var alumnos = from a in _context.Alumnos
                          where matriculados.Contains(a.Id)
                          select a;

            return alumnos;
        }

        //public IEnumerable<Alumno> GetAlumnos()
        //{
        //    var query = from a in _context.Alumnos
        //        select a;
        //    return query;
        //}
    }
}