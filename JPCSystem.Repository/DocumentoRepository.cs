using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private JpcSystemDbContext _context;

        public DocumentoRepository()
        {
            if (_context==null)
            {
                _context=new JpcSystemDbContext();;
            }
        }

        public IEnumerable<Documento> GetDocumentos()
        {
            var query = from c in _context.Documentos
                        select c;
            return query.OrderBy(d=>d.NomDocumento) ;
        }

        public Documento GetDocumento(int id)
        {
            return _context.Documentos.Find(id);
        }
    }
}
