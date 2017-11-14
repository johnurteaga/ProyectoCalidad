using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IDocumentoService
    {
        IEnumerable<Documento> GetDocumentos();
        Documento GetDocumento(int id);
    }
}
