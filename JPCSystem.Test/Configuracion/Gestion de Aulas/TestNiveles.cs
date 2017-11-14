using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using NUnit.Framework;
using JPCSystem.Web;
using JPCSystem.Service;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using JPCSystem.Domain;
using JPCSystem.Web.Controllers;

namespace JPCSystem.Test.Configuracion.Gestion_de_Aulas
{
    [Category("5 Configuracion Niveles")]
    [TestFixture]
    public class TestNiveles
    {
        Mock<INivelService> _serviceFalse;
        HttpContextBase context = _Helpers.GetFakeContext();
        //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        [SetUp]
        public void SetUp()
        {
            _serviceFalse = new Mock<INivelService>();
        }
        [Test]
        public void NombreNivelEsRequerido()
        {
            var controller = new NivelController(_serviceFalse.Object); controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            controller.Create(new Nivel
            {
                Id = 1,
               NombreNivel = ""
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NombreNivel"));
        }

        [Test]
        public void ListaNivel()
        {
            _serviceFalse.Setup(o => o.GetNiveles()).Returns(
                new List<Nivel>()
                {
                    new Nivel() {
                    },
                    new Nivel() {
                    },
                    new Nivel() {
                    },
                    new Nivel() {
                    }
                }
            );

            var controller = new NivelController(_serviceFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var resultado = controller.Index() as ViewResult;

            Assert.AreEqual(4, (resultado.Model as List<Nivel>).Count());

        }

        [Test]
        public void PuedoVerTodosLosNivels()
        {
            _serviceFalse.Setup(o => o.GetNiveles()).Returns(new List<Nivel>());
            var controller = new NivelController(_serviceFalse.Object);

            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }
        [Test]
        public void GuardarExitosoNivel()
            {
            var controller = new NivelController(_serviceFalse.Object);
            controller.Create(new Nivel()
            {
                Id = 1,
                NombreNivel = "Inicial"
            });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void GuardarFallidoNivel()
        {
            var controller = new NivelController(_serviceFalse.Object);
            controller.Create(new Nivel { });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        }

        [Test]
        public void GuardarRepetidoNivel()
        {
            var controller = new NivelController(_serviceFalse.Object);

            _serviceFalse.Setup(o => o.GetNiveles()).Returns(
                new List<Nivel>()
                {
                    new Nivel() {
                        Id = 1,
                        NombreNivel = "Inicial"
                    }});


            controller.Create(new Nivel()
            {
                Id = 2,
                NombreNivel = "Inicial"
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        }
    }
}
