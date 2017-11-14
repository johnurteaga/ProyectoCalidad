using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class RegNotasRepository : IRegNotasRepository
    {
        private JpcSystemDbContext _context;

        public RegNotasRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();
            }
        }

        public IEnumerable<AperturarRegistroNotas> GetRegistrosNotas(string criterio)
        {
            var query = from rn in _context.AperturarRegistroNotas.Include("AñoAcademico").Include("Trimestre")
                select rn;

            if (!String.IsNullOrEmpty(criterio))
            {
                query = from rn in query.Include("AñoAcademico").Include("Trimestre")
                        where rn.AñoAcademico.Anio.ToUpper().Contains(criterio.ToUpper())
                    select rn;
            }

            return query;
        }

        public AperturarRegistroNotas GetRegistroNotas(int id)
        {
            return _context.AperturarRegistroNotas.Find(id);
        }

        public void AddRegistroNotas(AperturarRegistroNotas aperturarRegistro)
        {
            _context.AperturarRegistroNotas.Add(aperturarRegistro);
            _context.SaveChanges();
        }

        public void UpdateRegistroNotas(AperturarRegistroNotas aperturarRegistro)
        {
            _context.Entry(aperturarRegistro).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRegistroNotas(int registroNotas)
        {
           var regNotas= _context.AperturarRegistroNotas.Find(registroNotas);
            if (regNotas!=null)
            {
                _context.Entry(regNotas).State= EntityState.Deleted;
                _context.SaveChanges();
            }
        }
    }
}
