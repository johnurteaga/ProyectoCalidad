using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPCSystem.Service;

namespace JPCSystem.Web.validaciones
{
    public class ValidacionesCurso
    {
        private readonly ModelStateDictionary _modelState;
        private readonly ICursoService _curso;
        public ValidacionesCurso(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public ValidacionesCurso(ModelStateDictionary modelState, ICursoService curso)
        {
            this._modelState = modelState;
            _curso = curso;
        }
        public void Required(string key, string value)
        {
            if (string.IsNullOrEmpty(value) || value == "Seleccione")
                _modelState.AddModelError(key, "Valor es requerido");
        }
        public void RequiredId(string key, int value)
        {
            if (value == 0)
                _modelState.AddModelError(key, "Valor es requerido");
        }
        public void RetipeNombreDeCursoAndIdNivel(string key, String value,int idNivel)
        {
            // var lista = );
            if (_curso.GetCursos("").Any(x => x.NombreCurso.Equals(value)) && _curso.GetCursos("").Any(x => x.NivelId.Equals(idNivel)))
                _modelState.AddModelError(key, "Ya Existe");
        }
    }
}