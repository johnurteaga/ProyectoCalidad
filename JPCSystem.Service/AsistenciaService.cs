using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class AsistenciaService : IAsistenciaService
    {
        private IAsistenciaRepository _repository;

        public AsistenciaService(IAsistenciaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Asistencia> GetAsistencias(string criterio)
        {
            return _repository.GetAsistencias(criterio);
        }

        public Asistencia GetAsistencia(int id)
        {
            return _repository.GetAsistencia(id);
        }

        public void AddAsistencia(Asistencia asistencia)
        {
            _repository.AddAsistencia(asistencia);
        }

        public void UpdateAsistencia(Asistencia asistencia)
        {
            _repository.UpdateAsistencia(asistencia);
        }
    }
}
