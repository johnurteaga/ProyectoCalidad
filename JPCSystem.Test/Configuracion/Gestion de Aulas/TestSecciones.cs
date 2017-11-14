using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Components.DictionaryAdapter.Xml;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace JPCSystem.Test.Configuracion.Gestion_de_Aulas
{
    [Category("7 Configuracion Secciones")]
    [TestFixture]
    public class TestSecciones
    {
        Mock<INivelService> _nivelFalse;
        Mock<IGradoService> _gradoFalse;
        Mock<ISeccionService> _seccionFalse;
        Mock<IMatriculaService> _matriculaFalse;
        Mock<IAlumnoService> _alumnoFalse;
        HttpContextBase context = _Helpers.GetFakeContext();
        //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        [SetUp]
        public void SetUp()
        {
            _nivelFalse= new Mock<INivelService>();
            _gradoFalse= new Mock<IGradoService>();
            _seccionFalse= new Mock<ISeccionService>();
            _matriculaFalse= new Mock<IMatriculaService>();
           _alumnoFalse= new Mock<IAlumnoService>();

           
          

            _matriculaFalse.Setup(o => o.GetMatriculas("")).Returns(
                new List<Matricula>()
                {
                    new Matricula()
                    {
                        SeccionId = 1,
                         AlumnoId = 1,
                          Id = 1,
                          AnioAcademicoId = 1,
                          ApoderadoId = 1,
                          FechaMatricula = DateTime.MaxValue,
                          nombreAnio = "2018"
                    },
                    new Matricula()  {
                        SeccionId = 1,
                        AlumnoId = 2,
                        Id = 2,
                        AnioAcademicoId = 1,
                        ApoderadoId = 1,
                        FechaMatricula = DateTime.MaxValue,
                        nombreAnio = "2018"
                    }
                }
            );

            _alumnoFalse.Setup(o => o.GetAlumnos("")).Returns(
                new List<Alumno>()
                {
                    new Alumno()
                    {
                        Id = 1,
                        ApMaterno ="rubio",
                        ApPaterno = "flores",
                        Correo = "cd@gmail.com",
                        Direccion = "cas",
                        DocumentoId = 12,
                        Estado = true,
                        FechaNacimiento = DateTime.Parse("24/12/2001"),
                        Genero = "Masculino",
                        IdUbigeo ="",
                        Nombre = "luis",
                        NumeroDocumento = 12121212,
                        Telefono = 12345678

                    },
                    new Alumno()
                    {
                        Id = 2,
                        ApMaterno ="rub2io",
                        ApPaterno = "flor2es",
                        Correo = "cd@gmai2l.com",
                        Direccion = "cas2",
                        DocumentoId = 11,
                        Estado = true,
                        FechaNacimiento = DateTime.Parse("24/12/2001"),
                        Genero = "Masculino",
                        IdUbigeo ="",
                        Nombre = "luis",
                        NumeroDocumento = 10121212,
                        Telefono = 12345678
                    }
                }
              );



        }

        //[Test]
        //public void ListaSeccion()
        //{
        //    _seccionFalse.Setup(o => o.GetSeccionesQueryable()).Returns(
        //        new List<Seccion>()
        //        {
        //            new Seccion() {
        //            Id = 1,
        //            NombreSeccion = "aaa",
        //            NivelId = 1,
        //            GradoId = 1,
        //            Capasida = 20,
        //            Alumnos = 1
        //            },
        //            new Seccion() {
        //                Id = 2,
        //                NombreSeccion = "bb",
        //                NivelId = 1,
        //                GradoId = 1,
        //                Capasida = 20,
        //                Alumnos = 1
        //            },
        //            new Seccion() {
        //                Id = 3,
        //                NombreSeccion = "bbb",
        //                NivelId = 1,
        //                GradoId = 1,
        //                Capasida = 20,
        //                Alumnos = 1
        //            }
        //        }.AsQueryable
        //    );
           
        //    var controller = new SeccionesController(_seccionFalse.Object,_nivelFalse.Object,_gradoFalse.Object,_matriculaFalse.Object);
        //    controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        //    var resultado = controller.Index(1) as JsonResult;

        //    Assert.AreEqual(3, (resultado.Data as List<Seccion>).Count);

          

        //}

        [Test]
        public void PuedoVerTodosLosSecciones()
        {
            var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);

            _seccionFalse.Setup(o => o.GetSeccionesQueryable()).Returns(new List<Seccion>().AsQueryable);
          controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var view = controller.Index(1) as PartialViewResult;

            Assert.IsInstanceOf<PartialViewResult>(view);
            Assert.IsNotNull(view.Model);
        }

        //[Test]
        //public void GradoIdEsRequerido()
        //{
        //    _seccionFalse.Setup(o => o.GetSeccionesQueryable()).Returns(
        //        new List<Seccion>()
        //        {
        //            new Seccion() {
        //                Id = 1,
        //                NombreSeccion = "aaa",
        //                NivelId = 1,
        //                GradoId = 1,
        //                Capasida = 20,
        //                Alumnos = 1
        //            },
        //            new Seccion() {
        //                Id = 2,
        //                NombreSeccion = "bb",
        //                NivelId = 1,
        //                GradoId = 1,
        //                Capasida = 20,
        //                Alumnos = 1
        //            },
        //            new Seccion() {
        //                Id = 3,
        //                NombreSeccion = "bbb",
        //                NivelId = 1,
        //                GradoId = 1,
        //                Capasida = 20,
        //                Alumnos = 1
        //            }
        //        }.AsQueryable
        //    );
        //    _nivelFalse.Setup(o => o.GetNiveles()).Returns(
        //        new List<Nivel>()
        //        {
        //            new Nivel
        //            {
        //                Id = 1,NombreNivel = "Inicial"
        //            },
        //            new Nivel
        //            {
        //                Id = 2,NombreNivel = "Primaria"
        //            }
        //        }
        //    );

        //    _gradoFalse.Setup(o => o.GetGrados()).Returns(
        //        new List<Grado>()
        //        {
        //            new Grado
        //            {
        //                Id = 1,
        //                NivelId = 2,
        //                NombreGrado = "Primero",
        //                numAlumnos = 20
        //            },
        //            new Grado { Id = 2,
        //                NivelId = 2,
        //                NombreGrado = "Segundo",
        //                numAlumnos = 20}
        //        }
        //    );
        //    var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);
        //    controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
        //    controller.Create(new Seccion()
        //    {
        //        Id = 1,
        //        NivelId = 1,
        //        GradoId = 0
        //    });
        //    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        //    Assert.True(controller.ViewData.ModelState.ContainsKey("GradoId"));
        //}
        //[Test]
        //public void NivelIdEsRequerido()
        //{
        //    var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);
        //    controller.Create(new Seccion()
        //    {
        //        Id = 1,
        //        NivelId = 0
        //    });
        //    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        //    Assert.True(controller.ViewData.ModelState.ContainsKey("NivelId"));
        //}
        //[Test]
        //public void NombreSeccionEsRequerido()
        //{
        //    var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);
        //    controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
        //    controller.Create(new Seccion()
        //    {
        //        Id = 1,
        //        NombreSeccion = ""
        //    });
        //    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        //    Assert.True(controller.ViewData.ModelState.ContainsKey("NombreSeccion"));
        //}


        //[Test]
        //public void GuardadoExitosoDeSeccion()
        //{
        //    _nivelFalse.Setup(o => o.GetNiveles()).Returns(
        //        new List<Nivel>()
        //        {
        //            new Nivel
        //            {
        //                Id = 1,NombreNivel = "Inicial"
        //            },
        //            new Nivel
        //            {
        //                Id = 2,NombreNivel = "Primaria"
        //            }
        //        }
        //    );

        //    _gradoFalse.Setup(o => o.GetGrados()).Returns(
        //        new List<Grado>()
        //        {
        //            new Grado
        //            {
        //                Id = 1,
        //                NivelId = 2,
        //                NombreGrado = "Primero",
        //                numAlumnos = 20
        //            },
        //            new Grado { Id = 2,
        //                NivelId = 2,
        //                NombreGrado = "Segundo",
        //                numAlumnos = 20}
        //        }
        //    );
        //    var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);
        //    controller.Create(new Seccion()
        //    {
        //        Id = 1,
        //        NombreSeccion = "aaa"
        //    });
        //    Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        //}
        //[Test]
        //public void GuardarFallidoSeccion()
        //{
        //    var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);
        //    controller.Create(new Seccion{ });
        //    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        //}

        //[Test]
        //public void GuardarRepetidoSeccion()
        //{
        //    var controller = new SeccionesController(_seccionFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _matriculaFalse.Object);
            
        //    _seccionFalse.Setup(o => o.GetSecciones("")).Returns(
        //        new List<Seccion>()
        //        {
        //            new Seccion() {
        //                Id = 1,
        //                NivelId = 1,
        //                NombreSeccion = "A",
        //                GradoId = 1,
        //                Capasida = 21,
        //                Alumnos = 2,

        //            }});


        //    controller.Create(new Seccion()
        //    {
        //        Id = 2,
        //        NivelId = 1,
        //        NombreSeccion = "A",
        //        GradoId = 1,
        //        Capasida = 21,
        //        Alumnos = 2
        //    });
        //    Assert.AreEqual(false, controller.ViewData.ModelState.IsValid);
        //}
    }
}
