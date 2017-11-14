using JPCSystem.Domain;
using JPCSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace JPCSystem.Web.validaciones
{
    public class ValidacionesDeGestionarAulas
    {
        private readonly ModelStateDictionary _modelState;
        private readonly INivelService _nivel;
        private readonly IGradoService _grado;
        private readonly ISeccionService _seccion;

        public ValidacionesDeGestionarAulas(ModelStateDictionary modelState, IGradoService grado)
        {
            this._modelState = modelState;
            _grado = grado;
        }
        public ValidacionesDeGestionarAulas(ModelStateDictionary modelState, INivelService service)
        {
            this._modelState = modelState;
            this._nivel = service;
        }
        public ValidacionesDeGestionarAulas(ModelStateDictionary modelState, ISeccionService service)
        {
            this._modelState = modelState;
            this._seccion = service;
        }
        //public ValidacionesDeGestionarAulas(ModelStateDictionary modelState)
        //{
        //    this._modelState = modelState;
        //}
        
        public void RequiredNombre(string key, String value)
        {
            if (string.IsNullOrEmpty(value))
                _modelState.AddModelError(key, "Valor requerido");
        }
        public void RequiredId(string key, int value)
        {
            if (value == 0)
                _modelState.AddModelError(key, "Valor es requerido");
        }

        public void RetipeNombreDeNivel(string key, String value)
        {
           // var lista = );
            if (_nivel.GetNiveles().Any(x => x.NombreNivel.Equals(value)))
                _modelState.AddModelError(key, "Ya Existe");
        }
        public void RetipeNombreDeGradoYNivel(string key, String value,int value2)
        {
            // var lista = );
            if (_grado.GetGrados().Any(x => x.NombreGrado.Equals(value)&&_nivel.GetNiveles().Any(y=>y.Id.Equals(value2))))
                _modelState.AddModelError(key, "Ya Existe");
        }
        public void RetipeNombreDeSeccion(string key, String value,string nivel,string grado)
        {
            // var lista = );
            if (_seccion.GetSecciones("").Any(x => x.NombreSeccion.Equals(value)&& _nivel.GetNiveles().Any(y=>y.NombreNivel.Equals(nivel))&& _grado.GetGrados().Any(z=>z.Equals(grado))))
                _modelState.AddModelError(key, "Ya Existe");
        }
    }
}