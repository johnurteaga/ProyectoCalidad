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
    [Category("001 Gestionar Docentes")]
    [TestFixture]
    public class TestGestionarDocente
    {
        Mock<IDocenteService> _docenteFalse;
        Mock<IDocumentoService> _documentoFalse;
        Mock<IUbigeoService> _ubigeoFalse;
        Mock<IUsuarioService> _usuarioFalse;



        HttpContextBase context = _Helpers.GetFakeContext();

        [SetUp]
        public void SetUp()
        {
            _docenteFalse = new Mock<IDocenteService>();
            _documentoFalse = new Mock<IDocumentoService>();
            _ubigeoFalse = new Mock<IUbigeoService>();
            _usuarioFalse = new Mock<IUsuarioService>();



        }
        [Test]
        public void ListaDeDocente()
        {
            _docenteFalse.Setup(o => o.GetDocentes("")).Returns(

                new List<Docente>()
                {
                    new Docente()
                    {

                        Id = 1,
                        ApMaterno = "Edwin",
                        ApPaterno = "Valda",
                        Direccion = "cas",
                        DocumentoId = 1,
                        EstadoCivil  = "Soltero",
                        FechaNacimiento = DateTime.Parse("24/12/2002"),
                        Genero = "Masculino",
                        IdUbigeo = "",
                        Nombres= "Alex",
                        NroDocumento  = 12121212,
                        Telefono = 12345678,
                        AniosExperiencia=10,
                    }
                }
            );

            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var resultado = controller.Index() as ViewResult;

            Assert.AreEqual(1, (resultado.Model as List<Docente>).Count);
        }

        [Test]
        public void GuardarFalloDocente()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            controller.Create(new Docente { });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        }

        [Test]
        public void GuardarDocenteExitoso()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object); controller.Create(new Docente()
                {

                    Id = 1,
                    ApMaterno = "Dias",
                    ApPaterno = "Velasquez",
                    Direccion = "cas",
                    DocumentoId = 1,
                    EstadoCivil = "S",
                    FechaNacimiento = DateTime.Parse("24/12/1998"),
                    Genero = "Masculino",
                    IdUbigeo = "151515",
                    Nombres = "Alex",
                    NroDocumento = 12121212,
                    Telefono = 125456782,
                    AniosExperiencia = 9

                });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }

        [Test]
        public void GuardarDocente_TelefonoRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            controller.Create(
                new Docente
                {
                    Telefono = 0
                 }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Telefono"));
        }
        [Test]
        public void GuardarDocente_NumeroDocumentoRequerido8digitos()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                    
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NroDocumento"));
        }
        [Test]
        public void GuardarDocente_AniosExperienciaRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                   

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("AniosExperiencia"));
        }
        [Test]
        public void GuardarDocente_ApMaternoRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                ApMaterno = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("ApMaterno"));
        }
        [Test]
        public void GuardarDocente_ApPaternoRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                   ApPaterno = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("ApPaterno"));
        }
        [Test]
        public void GuardarDocente_NombresRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                   Nombres = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Nombres"));
        }
        [Test]
        public void GuardarDocente_DireccionRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                    Direccion = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Direccion"));
        }
        [Test]
        public void GuardarDocente_DocumentoIdRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                    DocumentoId = 0

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("DocumentoId"));
        }
        [Test]
        public void GuardarDocente_EstadoCivilRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                  EstadoCivil = ""

                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("EstadoCivil"));
        }
        [Test]
        public void GuardarDocente_FechaNacimientoRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                    
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("FechaNacimiento"));
        }
        [Test]
        public void GuardarDocente_GeneroRequerido()
        {
            var controller = new DocenteController(
                _docenteFalse.Object,
                _documentoFalse.Object,
                _ubigeoFalse.Object, _usuarioFalse.Object);
            controller.Create(
                new Docente
                {
                    Genero = ""
                }
            );
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Genero"));
        }
    }
}
