using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class NotasRepository : INotasRepository
    {
        private JpcSystemDbContext _context;

        public NotasRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }

        public IEnumerable<Nota> GetNotas(string criterio)
        {
            var query = from n in _context.Nota.Include("Alumno").Include("Curso").Include("Seccion").Include("Seccion.Grado").Include("Seccion.Grado.Nivel")
                        select n;

            if (!String.IsNullOrEmpty(criterio))
            {
                query = from n in query.Include("Alumno").Include("Curso").Include("Seccion").Include("Seccion.Grado").Include("Seccion.Grado.Nivel")
                        where (n.Alumno.Nombre.ToUpper() +" "+ n.Alumno.ApPaterno.ToUpper()
                            +" "+n.Alumno.ApMaterno).Contains(criterio.ToUpper()) || 
                            (n.Alumno.ApPaterno.ToUpper() + " " + n.Alumno.ApMaterno +" " + 
                            n.Alumno.Nombre.ToUpper()).Contains(criterio.ToUpper()) || 
                            n.Alumno.Nombre.ToUpper().Contains(criterio.ToUpper()) ||
                             n.Alumno.ApPaterno.ToUpper().Contains(criterio.ToUpper()) ||
                              n.Alumno.ApMaterno.ToUpper().Contains(criterio.ToUpper()) || 
                              n.Seccion.Grado.Nivel.NombreNivel.ToUpper().Contains(criterio.ToUpper())
                        select n;
            }

            return query;

        }

        public Nota GetNota(int id)
        {
            return _context.Nota.Find(id);
        }

        public void AddNota(Nota nota)
        {
            _context.Nota.Add(nota);
            _context.SaveChanges();
        }

        public void UpdateNota(Nota nota)
        {
            _context.Entry(nota).State=EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteNota(int nota)
        {
            var notas = _context.Nota.Find(nota);

            if (notas!=null)
            {
                _context.Nota.Remove(notas);
            }
        }
    }
}
