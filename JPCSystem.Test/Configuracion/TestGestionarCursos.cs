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
    [Category("2 Gestionar Cursos")]
    [TestFixture]
    public class TestGestionarCursos
    {
        Mock<ICursoService> _cursoFalse;
        Mock<INivelService> _nivelFalse;
        Mock<IGradoService> _gradoFalse;
        HttpContextBase context = _Helpers.GetFakeContext();
        private CursoController controller;

        [SetUp]
        public void Init()
        {
            _cursoFalse = new Mock<ICursoService>();
            _nivelFalse = new Mock<INivelService>();
            _gradoFalse = new Mock<IGradoService>();
           // controller = new CursoController(_cursoFalse.Object,_nivelFalse.Object,_gradoFalse.Object);
        }

        [Test]
        public void ListaCursos()
        {
            _cursoFalse.Setup(o => o.GetCursos("")).Returns(
                new List<Curso>()
                {
                    new Curso() {
                        Id = 1,
                        NombreCurso = "Mate",
                        NivelId = 1,
                        GradoId = 1
                        },
                    new Curso() {
                        Id = 2,
                        NombreCurso = "Comu",
 
                        NivelId = 2,
                        GradoId = 1
                        },
                    new Curso() {
                        Id = 3,
                        NombreCurso = "Ingles",
                        NivelId = 3,
                        GradoId = 1
                    }
                }
            );

            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var resultado = controller.Index() as ViewResult;

            Assert.AreEqual(3, (resultado.Model as List<Curso>).Count());

        }

        [Test]
        public void PuedoVerTodosLosCursos()
        {
            _cursoFalse.Setup(o => o.GetCursos("")).Returns(new List<Curso>().AsQueryable);
            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }

      

        [Test]
        public void GuardadoExitosoDeCurso()
        {
            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            controller.Create(new Curso()
            {
                Id = 1,
                NivelId = 1,
                GradoId = 1,
                NombreCurso = "Mate"
            });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void GuardarRepetidoSeccion()
        {
            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            _cursoFalse.Setup(o => o.GetCursos("")).Returns(
                new List<Curso>()
                {
                    new Curso() {
                        Id = 1,
                        NivelId = 1,
                        GradoId = 1,
                        NombreCurso = "Mate"
                    }});


            controller.Create(new Curso()
            {
                Id = 2,
                NivelId = 1,
                GradoId = 1,
                NombreCurso = "Mate"
            });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void NombreCursoEsRequerido()
        {
            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            controller.Create(new Curso()
            {
                Id = 1,
                NivelId = 0,
                GradoId = 0,
                NombreCurso = ""
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NombreCurso"));
        }

        [Test]
        public void NivelIdEsRequerido()
        {
            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            controller.Create(new Curso()
            {
                Id = 1,
                NivelId = 0,
                GradoId = 1,
                NombreCurso = "",
            
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NivelId"));
        }

        [Test]
        public void GradoIdEsRequerido()
        {
            var controller = new CursoController(_cursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            controller.Create(new Curso()
            {
                Id = 1,
                NivelId = 0,
                GradoId = 0,
                NombreCurso = ""
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("GradoId"));
        }
    }
}
