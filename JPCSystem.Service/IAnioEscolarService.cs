using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IAnioEscolarService
    {
        IEnumerable<AnioAcademico> GetAniosAcademicos(String criterio);

        AnioAcademico GetAnioAcademico(Int32 id);

        void AddAnioAcademico(AnioAcademico anioAcademico);

        void UpdateAnioAcademico(AnioAcademico anioAcademico);

        void DeleteAnioAcademico(Int32 anioAcademico);

    }
}
