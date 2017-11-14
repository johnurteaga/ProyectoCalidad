using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JPCSystem.Test.Usuarios
{
    [Category("001 Gestionar Alumno")]
    [TestFixture]
    public class TestGestionarAlumno
    {
        Mock<IAlumnoService> _alumnoFalse;
        Mock<IDocumentoService> _documentoFalse;
        Mock<IUbigeoService> _ubigeoFalse;
        Mock<IUsuarioService> _usuarioFalse;



        HttpContextBase context = _Helpers.GetFakeContext();

        [SetUp]
        public void SetUp()
        {
            _alumnoFalse = new Mock<IAlumnoService>();
            _documentoFalse = new Mock<IDocumentoService>();
            _ubigeoFalse = new Mock<IUbigeoService>();
            _usuarioFalse = new Mock<IUsuarioService>();

        }

        [Test]
        public void ListaDeAlumnos()
        {
            _alumnoFalse.Setup(o => o.GetAlumnos("")).Returns(
                new List<Alumno>()
                {
                    new Alumno
                    {

                        Id = 1,
                        ApMaterno = "Edwin",
                        ApPaterno = "Valdivia",
                        Correo = "cd@gmail.com",
                        Direccion = "cas",
                        DocumentoId = 1,
                        Estado = true,
                        FechaNacimiento = DateTime.Parse("24/12/2001"),
                        Genero = "Masculino",
                        IdUbigeo = "",
                        Nombre = "luis",
                        NumeroDocumento = 12121212,
                        Telefono = 12345678,


                    },
                    new Alumno
                    {

                        Id = 2,
                        ApMaterno = "rubio",
                        ApPaterno = "flores",
                        Correo = "cd@gmail.com",
                        Direccion = "cas",
                        DocumentoId = 12,
                        Estado = true,
                        Genero = "Masculino",
                        IdUbigeo = "",
                        Nombre = "luis",
                        NumeroDocumento = 12121212,
                        Telefono = 12345678,
                    }
                });

            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var resultado = controller.Index() as ViewResult;

            Assert.AreEqual(2, (resultado.Model as List<Alumno>).Count);
        }
  

//falta solucionar
[Test]
public void GuardarExitosoAlumno()
{
    var controller = new AlumnoController(
        _alumnoFalse.Object,
        _documentoFalse.Object,
        _usuarioFalse.Object, _ubigeoFalse.Object); controller.Create(new Alumno()
    {
        Id = 1,
        ApMaterno = "Caceres",
        ApPaterno = "flores",
        Correo = "cd@gmail.com",
        Direccion = "cas",
        DocumentoId = 1,
        Estado = true,
        FechaNacimiento = DateTime.Parse("24/12/2001"),
        Genero = "Femenino",
        IdUbigeo = "250204",
        Nombre = "Juana",
        NumeroDocumento = 12121212,
        Telefono = 12345678,
    });
    Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
}
[Test]
public void GuardarFalloAlumno()
{
    var controller = new AlumnoController(
        _alumnoFalse.Object,
        _documentoFalse.Object,
        _usuarioFalse.Object, _ubigeoFalse.Object); controller.Create(new Alumno { });
    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
}


        //para revisar
        [Test]
        public void ExcedeOFatalCantidadDeDigitosAlDNI()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {
                    NumeroDocumento = int.Parse("111111250")
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NumeroDocumento"));

        }


        [Test]
        public void GuardarAlumno_NumeroDocumentoRequerido8digitos()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {
                    NumeroDocumento = 123456789
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NumeroDocumento"));
        }

        [Test]
        public void GuardarAlumno_ApPaternoRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {
                    ApMaterno = "1212"

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("ApPaterno"));
        }

        [Test]
        public void GuardarAlumno_ApMaternoRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {
                    ApMaterno = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("ApMaterno"));
        }
        [Test]
        public void GuardarAlumno_NombreRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {

                    Nombre = ""
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Nombre"));
        }


        [Test]
        public void GuardarAlumno_DireccionRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {


                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Direccion"));
        }

        [Test]
        public void GuardarAlumno_CorreoRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);

            controller.Create(
                new Alumno
                {

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Correo"));
        }

        [Test]
        public void GuardarAlumno_FechaNacimientoRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {
                    FechaNacimiento = DateTime.Parse("24/12/1996")
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("FechaNacimiento"));
        }

        [Test]
        public void GuardarAlumno_GeneroRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);
            controller.Create(
                new Alumno
                {
                    Genero = ""
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Genero"));
        }

        [Test]
        public void GuardarAlumno_TelefonoRequerido()
        {
            var controller = new AlumnoController(
                _alumnoFalse.Object,
                _documentoFalse.Object,
                _usuarioFalse.Object, _ubigeoFalse.Object);

            controller.Create(
                new Alumno
                {
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Telefono"));
        }


    }
}
