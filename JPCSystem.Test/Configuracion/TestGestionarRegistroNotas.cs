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
    [Category("3 Apertura Registro de Notasr")]
    [TestFixture]
    public class TestGestionarRegistroNotas
    {
        Mock<IRegNotasService> _regNotasFalse;
        Mock<IAnioEscolarService> _anioEscolarFalse;
        Mock<ITrimestreService> _TrimestreFalse;
        HttpContextBase context = _Helpers.GetFakeContext();

        private ApRegNotasController controller;
        //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        [SetUp]
        public void SetUp()
        {
            _regNotasFalse = new Mock<IRegNotasService>();
            _anioEscolarFalse = new Mock<IAnioEscolarService>();
            _TrimestreFalse = new Mock<ITrimestreService>();
            controller = new ApRegNotasController(_regNotasFalse.Object, _anioEscolarFalse.Object, _TrimestreFalse.Object);
            // controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
        }
        [Test]
        public void GuardarAperturaraRegistroNotas()
        {

            controller.Create(new AperturarRegistroNotas()
            {
                Id = 1,
                AñoAcademico = new AnioAcademico(),
                AñoAcademicoId = 1,
                FechaFinPrimTrim = DateTime.MaxValue,
                FechaInicioPrimTrim = DateTime.MaxValue,
                Trimestre = new Trimestre(),
                TrimestreId = 1,
                estado = true,
                nombreAño = ToString()
            });
            Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        }
        [Test]
        public void ListaDeAperturaRegNotas()
        {
            _regNotasFalse.Setup(o => o.GetRegistrosNotas("")).Returns(
                new List<AperturarRegistroNotas>()
                {
                    new AperturarRegistroNotas {
                        Id = 1,
                        AñoAcademico = new AnioAcademico(),
                        AñoAcademicoId = 1,
                        FechaFinPrimTrim = DateTime.MaxValue,
                        FechaInicioPrimTrim = DateTime.MaxValue,
                        Trimestre = new Trimestre(),
                        TrimestreId = 1,
                        estado =true,
                        nombreAño = ToString()},
                    new AperturarRegistroNotas {
                        Id = 2,
                        AñoAcademico = new AnioAcademico(),
                        AñoAcademicoId = 1,
                        FechaFinPrimTrim = DateTime.MaxValue,
                        FechaInicioPrimTrim = DateTime.MaxValue,
                        Trimestre = new Trimestre(),
                        TrimestreId = 1,
                        estado =true,
                        nombreAño = ToString()}
                }
            );

            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            var resultado = controller.Index("") as PartialViewResult;

            Assert.AreEqual(2, (resultado.Model as List<AperturarRegistroNotas>).Count());

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
            _regNotasFalse.Setup(o => o.GetRegistrosNotas("")).Returns(new List<AperturarRegistroNotas>());
            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }

        [Test]
        public void FechaInicioPrimTrimEsRequeridoDentroDeRangoConAnio()
        {
            _anioEscolarFalse.Setup(o => o.GetAniosAcademicos("")).Returns(
                new List<AnioAcademico> {
                    new AnioAcademico
                    {
                        Id = 1,
                        Anio = "2017"
                       
                    },
                    new AnioAcademico { Id =2}
                }
            );
            controller.Create(new AperturarRegistroNotas()
            {
                Id = 1,
                FechaInicioPrimTrim = DateTime.Parse("10/06/2018"),
                AñoAcademicoId = 1

            });
        
            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("FechaInicioPrimTrim"));
        }
        [Test]
        public void FechaFinPrimTrimEsRequeridoDentroDeRangoConAnio()
        {
            _anioEscolarFalse.Setup(o => o.GetAniosAcademicos("")).Returns(
                new List<AnioAcademico> {
                    new AnioAcademico
                    {
                        Id = 1,
                        Anio = "2017"

                    },
                    new AnioAcademico { Id =2}
                }
            );
            controller.Create(new AperturarRegistroNotas()
            {
                Id = 1,
                //FechaInicioPrimTrim = DateTime.Parse("10/06/2018"),
                FechaFinPrimTrim = DateTime.Parse("10/07/2018"),
                AñoAcademicoId = 1

            });

            Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
            Assert.True(controller.ViewData.ModelState.ContainsKey("FechaFinPrimTrim"));
        }



    }
}
