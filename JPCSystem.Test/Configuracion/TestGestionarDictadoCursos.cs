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
    [Category("4 Dictado de Cursos")]
    [TestFixture]
    public class TestGestionarDictadoCursos
    {
        Mock<IDocenteCursoService> _docenteCursoFalse;
        Mock<INivelService> _nivelFalse;
        Mock<IGradoService> _gradoFalse;
        Mock<IDocenteService> _docenteFalse;
        Mock<ICursoService> _cursoFalse;
        Mock<ISeccionService> _seccionFalse;
        HttpContextBase context = _Helpers.GetFakeContext();

        private DocenteCursoController controller;
        //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        [SetUp]
        public void SetUp()
        {
            _docenteCursoFalse = new Mock<IDocenteCursoService>();
            _nivelFalse = new Mock<INivelService>();
            _gradoFalse = new Mock<IGradoService>();
            _docenteFalse = new Mock<IDocenteService>();
            _cursoFalse = new Mock<ICursoService>();
            _seccionFalse = new Mock<ISeccionService>();
            controller = new DocenteCursoController(_docenteCursoFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _docenteFalse.Object, _cursoFalse.Object, _seccionFalse.Object);
            // controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
        }

        [Test]
        public void NivelIdEsRequerido()
        {
           controller.Create(new DocenteCurso()
            {
                Id = 1,
                NivelId = 0
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("NivelId"));
        }

        [Test]
        public void GradoIdEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
                NivelId = 1,
                GradoId = 0
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("GradoId"));
        }
        [Test]
        public void SeccionIdEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
                NivelId = 1,
                GradoId = 1,
                SeccionId = 0
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("SeccionId"));
        }
        [Test]
        public void CursoIdEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
                NivelId = 1,
                GradoId = 1,
                SeccionId = 1,
                CursoId = 0
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("CursoId"));
        }
        [Test]
        public void DocenteIdEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
                DocenteId = 0
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("DocenteId"));
        }
        [Test]
        public void HoraInicioEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
               HoraInicio = ""
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("HoraInicio"));
        }
        [Test]
        public void HoraFinEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
                HoraFin = ""
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("HoraFin"));
        }
        [Test]
        public void DiaEsRequerido()
        {
            controller.Create(new DocenteCurso()
            {
                Id = 1,
                Dia = ""
            });
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("Dia"));
        }




        [Test]
        public void GuardarDictadoDeClases()
        {

            controller.Create(new DocenteCurso()
            {
                Id = 1,
                Seccion = new Seccion(),
                NivelId = 1,
                Curso = new Curso(),
                GradoId = 1,
                SeccionId = 1,
                CursoId = 1,
                Dia = ToString(),
                Docente = new Docente(),
                DocenteId = 1,
                HoraFin = ToString(),
                HoraInicio = ToString()

            });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void ListaDeCursosADictar()
        {
            _docenteCursoFalse.Setup(o => o.GetDocenteCursos("")).Returns(
                new List<DocenteCurso>()
                {
                    new DocenteCurso() {
                        Id = 1,
                        Seccion = new Seccion(),
                        NivelId = 1,
                        Curso = new Curso(),
                        GradoId = 1,
                        SeccionId = 1,
                        CursoId = 1,
                        Dia = ToString(),
                        Docente = new Docente(),
                        DocenteId = 1,
                        HoraFin = ToString(),
                        HoraInicio = ToString()
                    },
                    new DocenteCurso() {
                        Id = 2,
                        Seccion = new Seccion(),
                        NivelId = 1,
                        Curso = new Curso(),
                        GradoId = 1,
                        SeccionId = 1,
                        CursoId = 1,
                        Dia = ToString(),
                        Docente = new Docente(),
                        DocenteId = 1,
                        HoraFin = ToString(),
                        HoraInicio = ToString()}
                }
            );

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            var resultado = controller.Index() as ViewResult;

            Assert.AreEqual(2, (resultado.Model as List<DocenteCurso>).Count());

        }
        [Test]
        public void PuedoVerFormularioDeReportedeCursos()
        {
            var view = controller.Create();

            Assert.IsInstanceOf<ActionResult>(view);
            Assert.IsNotNull(view);
        }

        [Test]
        public void PuedoVerTodosLosCursosADictar()
        {
            _docenteCursoFalse.Setup(o => o.GetDocenteCursos("")).Returns(new List<DocenteCurso>());
            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }

    }
}
