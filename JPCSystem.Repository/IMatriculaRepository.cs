using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface IMatriculaRepository
    {
        IEnumerable<Matricula> GetMatriculas(string criterio);

        Matricula GetMatricula(Int32 id);

        void AddMatricula(Matricula matricula);

        void UpdateMatricula(Matricula matricula);

        void DeleteMatricula(Int32 matricula);

        IEnumerable<Alumno> GetAlumnos(Int32 seccionId);

        IEnumerable<Alumno> GetAlumnosNotas(Int32 seccionId, Int32 cursoId);

        //IEnumerable<Alumno> GetAlumnos();
    }
}
