using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace JPCSystem.Test.Usuarios
{
    [Category("001 Gestionar Apoderado")]
    [TestFixture]
    public class TestGestionarApoderado
    {
         Mock<IAlumnoService> _alumnoFalse;
        Mock<IDocumentoService> _documentoFalse;
        Mock<IUbigeoService> _ubigeoFalse;
        Mock<IUsuarioService> _usuarioFalse;
        Mock<IApoderadoService> _apoderadoFalse;



        HttpContextBase context = _Helpers.GetFakeContext();

        [SetUp]
        public void SetUp()
        {
            _alumnoFalse = new Mock<IAlumnoService>();
            _documentoFalse = new Mock<IDocumentoService>();
            _ubigeoFalse = new Mock<IUbigeoService>();
            _usuarioFalse = new Mock<IUsuarioService>();
            _apoderadoFalse= new Mock<IApoderadoService>();


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
                        IdUbigeo ="120907",
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
                        IdUbigeo ="120907",
                        Nombre = "luis",
                        NumeroDocumento = 12121212,
                        Telefono = 12345678,
                    }
                });

        }

        [Test]
        public void ListaDeApoderados()
        {
            _apoderadoFalse.Setup(o => o.GetApoderados("")).Returns(
            
                new List<Apoderado>()
                {
                    new Apoderado()
                        {
                            Id = 1,
                            ApPaterno = "VALDIVIA",
                            Correo = "valdivia@upn.pe",
                            Genero = "M",
                            FechaNacimiento = DateTime.Parse("24/12/1980"),
                            IdUbigeo ="120907",
                            DocumentoId = 1,
                            ApMaterno = "VALDIVIA",
                            Direccion = "las flores",
                            Telefono = 991856523,
                            LugarTrabajo = ToString(),
                            Nombres = "JUAN",
                            NroDocumento = 15151515
                        }
                }
            );

            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);

           controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var resultado = controller.Index() as ViewResult;

            Assert.AreEqual(1, (resultado.Model as List<Apoderado>).Count);
        }

        //falta solucionar
        [Test]
        public void GuardarExitosoApoderado()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                    Id = 1,
                    ApPaterno = "VALDIVIA",
                    ApMaterno = "VALDIVIA",
                    Direccion = "las flores",
                    Correo = "valdivia@upn.pe",
                    Genero = "M",
                    FechaNacimiento = DateTime.Parse("24/12/1989"),
                    IdUbigeo ="120907",
                    DocumentoId = 1,
                    Telefono = 991856523,
                    LugarTrabajo = "DRC",
                    Nombres = "JUAN",
                    NroDocumento = 15151515

                });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void GuardarFalloApoderado()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(new Apoderado{ });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        }


        //para revisar
        [Test]
        public void ExcedeOFatalCantidadDeDigitosAlDNI()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                    NroDocumento= int.Parse("111111250")
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NroDocumento"));

        }


        [Test]
        public void GuardarApoderado_NumeroDocumentoRequerido8digitos()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                    NroDocumento = 123456789
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NroDocumento"));
        }

        [Test]
        public void GuardarApoderado_ApPaternoRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                    ApMaterno = "1212"

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("ApPaterno"));
        }

        [Test]
        public void GuardarApoderado_ApMaternoRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                    ApMaterno = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("ApMaterno"));
        }
        [Test]
        public void GuardarApoderado_NombreRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {

                    Nombres = ""
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Nombres"));
        }


        [Test]
        public void GuardarApoderado_DireccionRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Direccion"));
        }

        [Test]
        public void GuardarApoderado_CorreoRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Correo"));
        }

        [Test]
        public void GuardarApoderado_FechaNacimientoRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                   
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("FechaNacimiento"));
        }

        [Test]
        public void GuardarApoderado_GeneroRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                    Genero = ""
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Genero"));
        }

        [Test]
        public void GuardarApoderado_TelefonoRequerido()
        {
            var controller = new ApoderadoController(
                _apoderadoFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object);
            controller.Create(
                new Apoderado()
                {
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Telefono"));
        }


    }
}