using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class DocumentoService :IDocumentoService
    {
        private IDocumentoRepository _repository;

        //public DocumentoService()
        //{
        //    if (_repository == null)
        //    {
        //        _repository = new DocumentoRepository();
        //    }
        //}

        public DocumentoService(IDocumentoRepository documentoRepository)
        {
            _repository = documentoRepository;
        }

        public IEnumerable<Documento> GetDocumentos()
        {
            return _repository.GetDocumentos();
        }

        public Documento GetDocumento(int id)
        {
            return _repository.GetDocumento(id);
        }
    }
}
