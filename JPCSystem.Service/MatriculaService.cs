using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class MatriculaService : IMatriculaService
    {
        private IMatriculaRepository _repository;

        public MatriculaService(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Matricula> GetMatriculas(string criterio)
        {
            return _repository.GetMatriculas(criterio);
        }

        public Matricula GetMatricula(int id)
        {
            return _repository.GetMatricula(id);
        }

        public void AddMatricula(Matricula matricula)
        {
            _repository.AddMatricula(matricula);
        }

        public void UpdateMatricula(Matricula matricula)
        {
            _repository.UpdateMatricula(matricula);
        }

        public void DeleteMatricula(int matricula)
        {
            _repository.DeleteMatricula(matricula);
        }

        public IEnumerable<Alumno> GetAlumnos(int seccionId)
        {
            return _repository.GetAlumnos(seccionId);
        }
    }
}
