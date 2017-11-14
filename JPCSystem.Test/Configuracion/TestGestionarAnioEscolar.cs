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

namespace JPCSystem.Test.Configuracion
{
    [Category("1 Gestionar Año Escolar")]
    [TestFixture]
    public class TestGestionarAnioEscolar
    {
        Mock<IAnioEscolarService> _anioFalse;
        HttpContextBase context = _Helpers.GetFakeContext();
        private AnioEscolarController controller;

        [SetUp]
        public void Init()
        {
            _anioFalse = new Mock<IAnioEscolarService>();
            controller = new AnioEscolarController(_anioFalse.Object);

        }

        [Test]
        public void GuardarAnioEscolar()
        {

            controller.Create(new AnioAcademico
            {
                Id = GetHashCode(),
                FechaInicioMatricula = DateTime.MaxValue,
                FechaInicio = DateTime.MaxValue,
                Descripcion = ToString(),
                FechaFin = DateTime.MaxValue,
                FechaMatriculaExtemporanea = DateTime.MaxValue,
                Anio = ToString(),
                FechaFinMatricula = DateTime.MaxValue
            });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void ListaDeAperturaRegNotas()
        {
            _anioFalse.Setup(o => o.GetAniosAcademicos("")).Returns(
                new List<AnioAcademico>()
                {
                    new AnioAcademico {
                        Id = GetHashCode(),
                        FechaInicioMatricula = DateTime.MaxValue,
                        FechaInicio = DateTime.MaxValue,
                        Descripcion = ToString(),
                        FechaFin = DateTime.MaxValue,
                        FechaMatriculaExtemporanea = DateTime.MaxValue,
                        Anio = ToString(),
                        FechaFinMatricula = DateTime.MaxValue},
                    new AnioAcademico {
                        Id = GetHashCode(),
                        FechaInicioMatricula = DateTime.MaxValue,
                        FechaInicio = DateTime.MaxValue,
                        Descripcion = ToString(),
                        FechaFin = DateTime.MaxValue,
                        FechaMatriculaExtemporanea = DateTime.MaxValue,
                        Anio = ToString(),
                        FechaFinMatricula = DateTime.MaxValue}
                }
            );

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            var resultado = controller.Index("") as PartialViewResult;

            Assert.AreEqual(2, (resultado.Model as List<AnioAcademico>).Count());

        }
        [Test]
        public void PuedoVerFormularioDeApRegNotas()
        {
            var view = controller.Create();

            Assert.IsInstanceOf<ActionResult>(view);
            Assert.IsNotNull(view);
        }

        [Test]
        public void PuedoVerTodosLosAniosAcademicos()
        {
            _anioFalse.Setup(o => o.GetAniosAcademicos("")).Returns(new List<AnioAcademico>());
            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }

    }
}
