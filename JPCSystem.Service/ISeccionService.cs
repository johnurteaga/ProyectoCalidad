using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface ISeccionService
    {
        IEnumerable<Seccion> GetSecciones(string criterio);

        IQueryable<Seccion> GetSeccionesQueryable();

        Seccion GetSeccion(Int32 id);

        void AddSeccion(Seccion seccion);

        void UpdateSeccion(Seccion seccion);

        void DeleteSeccion(Int32 seccion);
    }
}
