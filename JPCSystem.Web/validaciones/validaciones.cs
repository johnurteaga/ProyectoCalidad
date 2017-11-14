using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace JPCSystem.Web.validaciones
{
    public class ValidacionesUsuario
    {
        private readonly ModelStateDictionary _modelState;


        public ValidacionesUsuario(ModelStateDictionary modelState)
        {
            this._modelState = modelState;
        }

        Regex regEmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        public void Required(string key, string value)
        {
            if (string.IsNullOrEmpty(value) || value== "Seleccione" || value == "[-- Selecciona Documento --]" )
                _modelState.AddModelError(key, "Valor es requerido");
        }

        public void RequiredId(string key, int value)
        {
            if (value == 0)
                _modelState.AddModelError(key, "Valor es requerido");
        }

        public void RequiredFechas(string key, DateTime value)
        {
            if (value == DateTime.Parse("1/01/0001"))
                _modelState.AddModelError(key, "Valor es requerido o fuera de rango");
        }

        public void MaxLength(string key, string value, int n)
        {
            if (!string.IsNullOrEmpty(value) )
            {
                if (value.Length >= n)
                {
                    _modelState.AddModelError(key, string.Format("Valor no debe exceder los {0} caracteres", n));
                }
            }
            
        }

        public void MaxLengthDocumento(string key, string value, int n)
        {
            if (!string.IsNullOrEmpty(value))
            {
                
                if (value.Length > n || value.Length < n)
                {
                    var estado = value.Length > n ? "mayor" : "menor";
                    _modelState.AddModelError(key, string.Format("Valor no debe ser "+ estado +" a los {0} caracteres", n));
                }
            }

        }

        public void numeroRepetido(string key, int n)
        {
            if (n > 0)
                _modelState.AddModelError(key, "valor ya registrado");
        }

        public void FechaMayor(string key, DateTime value)
        {
            var menor = 1825;
            var mayor = 6574;
            var ingreso = int.Parse((DateTime.Now - value).Days.ToString());

            if (ingreso> mayor)
            {
                _modelState.AddModelError(key, "La edad del alumno dever ser menor a 18 años");
            }
            if (ingreso <= menor)
            {
                _modelState.AddModelError(key, "La edad del alumno deve ser mayor a 5 años.");
            }
        }

        public void FechaIngresoNotasMayor(string key, DateTime value)
        {
            var valido = DateTime.Now.Date - value;
            if (key == "FechaNacimiento" && valido.Days >= 1825)
            {
                _modelState.AddModelError(key, "El Alumno no puede ser matriculado");
            }
        }


        public void RequiredNro(string key, int value)
        {
            if (value == 0)
                _modelState.AddModelError(key, "Valor requerido");
        }

        public void Email(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!regEmail.IsMatch(value))
                    _modelState.AddModelError("Correo", "Correo no valido, ejemplo: aabbcc@ejemplo.com");
            }
            

        }

        public void VerificaRangos(string key, DateTime valueInicio, DateTime valueFin)
        {
            if (valueInicio>=valueFin)
                _modelState.AddModelError(key, "La fecha se encuentra fuera de rango");
            
        }

        public void VerificaRangosF_E(string key, DateTime valueInicio, DateTime valueFin)
        {
            if (valueInicio <= valueFin)
                _modelState.AddModelError(key, "La fecha se encuentra fuera de rango");

        }
        public void VerificaFechaConAnioAcademico(string key, DateTime valueInicio, string anio)
        {
            if (int.Parse(valueInicio.Year.ToString()) != int.Parse(anio))
                _modelState.AddModelError(key, "La fecha no pertenece al mismo año");

        }

        public void FechaMayorApoderado(string key, DateTime value)
        {
            var mayor = 6574;
            var ingreso = int.Parse((DateTime.Now - value).Days.ToString());

            if (ingreso <= mayor)
            {
                _modelState.AddModelError(key, "La edad del alumno dever ser mayor a 18 años");
            }
        }
    }
}