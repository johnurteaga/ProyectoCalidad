using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace JPCSystem.Test.Configuracion.Gestion_de_Aulas
{

    [NUnit.Framework.Category("6 Configuracion Grados")]
    [TestFixture]
    public class TestGrados
    {
        Mock<IGradoService> _serviceFalse;
        Mock<INivelService> _serviceNivelFalse;
        HttpContextBase context = _Helpers.GetFakeContext();
        //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        [SetUp]
        public void SetUp()
        {
            _serviceFalse = new Mock<IGradoService>();
            _serviceNivelFalse= new Mock<INivelService>();
        }

        [Test]
        public void ListaDeGrados()
        {
            _serviceFalse.Setup(o => o.GetGrados()).Returns(
                new List<Grado>()
                {
                    new Grado {
                        Nivel = new Nivel(),
                        NombreGrado = "Primero",
                        Id = 1,
                        NivelId= 1,
                        numAlumnos= 12

                    },
                    new Grado {
                        Nivel = new Nivel(),
                        NombreGrado = "Primero",
                        Id = 1,
                        NivelId= 1,
                        numAlumnos= 12
                    }
                }
            );

            var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            var resultado = controller.Index("") as PartialViewResult;

            Assert.AreEqual(2, (resultado.Model as List<Grado>).Count());

        }

        [Test]
        public void PuedoVerTodosLosGrados()
        {
            _serviceFalse.Setup(o => o.GetGrados()).Returns(new List<Grado>());
            var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);

            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }

        [Test]
        public void NombreGradoEsRequerido()
        {
            var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);

            controller.Create(new Grado()
            {
                Id = 1,
               NivelId = 1,
               NombreGrado = ""
            },1);
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NombreGrado"));
        }


        [Test]
        public void NivelIdEsRequerido()
        {
            var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);

            controller.Create(new Grado()
            {
                Id = 1,
                NivelId = 0,
                NombreGrado = ""
            }, 1);
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NivelId"));
        }

        [Test]
        public void GuardarExitosoGrado()
        {
            var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);

            controller.Create(new Grado()
            {
                Id = 1,
                NombreGrado = "Inicial",
                Nivel = new Nivel(),
                NivelId = 1,
                numAlumnos = 23
            }, 1);
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void GuardarFallidoGrado()
        {
            var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);
            controller.Create(new Grado { }, 1);
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        }

        //[Test]
        //public void GuardarRepetidoGrado()
        //{
        //    var controller = new GradosController(_serviceFalse.Object, _serviceNivelFalse.Object);
        //    _serviceFalse.Setup(o => o.GetGrados()).Returns(
        //        new List<Grado>()
        //        {
        //            new Grado() {
        //                Id = 1,
        //                Nivel =  new Nivel(),

        //                NombreGrado = "Primero",
        //                NivelId = 1,
        //                numAlumnos = 12
                        
        //            }});


        //    controller.Create(new Grado()
        //    {
        //        Id = 2,
        //        Nivel = new Nivel(),
        //        NombreGrado = "Primero",
        //        NivelId = 1,
        //        numAlumnos = 12
        //    },1);
        //    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        //}
    }
}
