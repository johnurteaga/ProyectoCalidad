using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface IAsistenciaRepository
    {
        IEnumerable<Asistencia> GetAsistencias(string criterio);
        Asistencia GetAsistencia(int id);
        void AddAsistencia(Asistencia asistencia);
        void UpdateAsistencia(Asistencia asistencia);

    }
}
