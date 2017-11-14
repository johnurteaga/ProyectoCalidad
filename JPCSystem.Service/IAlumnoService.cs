using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IAlumnoService
    {
        IEnumerable<Alumno> GetAlumnos(string criterio);

        Alumno GetAlumno(Int32 id);

        void AddAlumno(Alumno alumno);

        void UpdateAlumno(Alumno alumno);

        void DeleteAlumno(Int32 alumno);

    }
}
