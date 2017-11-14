using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class AlumnoService : IAlumnoService
    {
        private IAlumnoRepository _repository;

        //Nueva forma de realizar una conexion con 
        //el Repository aplicando dependencias 
        public AlumnoService(IAlumnoRepository alumnoRepository)
        {
            _repository = alumnoRepository;
        }

        public void AddAlumno(Alumno alumno)
        {
            _repository.AddAlumno(alumno);
        }

        public void DeleteAlumno(int alumno)
        {
            _repository.DeleteAlumno(alumno);
        }

        public Alumno GetAlumno(int id)
        {
            return _repository.GetAlumno(id);
        }

        public IEnumerable<Alumno> GetAlumnos(string criterio)
        {
            return _repository.GetAlumnos(criterio);
        }

        public void UpdateAlumno(Alumno alumno)
        {
            _repository.UpdateAlumno(alumno);
        }


    }
}
