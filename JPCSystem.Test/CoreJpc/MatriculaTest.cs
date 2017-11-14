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

namespace JPCSystem.Test.CoreJpc
{
    [Category("01 Matricula")]
    [TestFixture]
    public class MatriculaTest
    {

     
        Mock<IAlumnoService> _alumnoFalse;
        Mock<IMatriculaService> _matriculaFalse;
        Mock<IDocumentoService> _documentoFalse;
        Mock<IGradoService> _gradoFalse;
        Mock<INivelService> _nivelFalse;
        Mock<ISeccionService> _seccionFalse;
        Mock<IApoderadoService> _apoderadoFalse;
        Mock<IAnioEscolarService> _anioFalse;
        Mock<IPromedioService> _promedioFalse;
        Mock<IUbigeoService> _ubigeoFalse;

        HttpContextBase context = _Helpers.GetFakeContext();

        //private MatriculaController controller;
        //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

        [SetUp]
        public void SetUp()
        {
            _alumnoFalse = new Mock<IAlumnoService>();
            _matriculaFalse = new Mock<IMatriculaService>();
            _documentoFalse = new Mock<IDocumentoService>();
            _nivelFalse = new Mock<INivelService>();
            _gradoFalse = new Mock<IGradoService>();
            _seccionFalse = new Mock<ISeccionService>();
            _apoderadoFalse = new Mock<IApoderadoService>();
            _anioFalse = new Mock<IAnioEscolarService>();
            _promedioFalse = new Mock<IPromedioService>();

            //----------------------------------------------------------------

          
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
                        Telefono = 12345678

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
                        Telefono = 12345678
                    }
            }
                );
            _apoderadoFalse.Setup(o => o.GetApoderados("")).Returns(
                new List<Apoderado>() {
                new Apoderado()
                {
                    Id = 1,
                    ApPaterno = "VALDIVIA",
                    Correo = "valdivia@upn.pe",
                    Genero = "M",
                    FechaNacimiento = DateTime.Parse("24/12/1980"),
                    IdUbigeo = "",
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
            _nivelFalse.Setup(o => o.GetNiveles()).Returns(

                new List<Nivel>
                {
                    new Nivel
                    {
                        Id = 1,
                        NombreNivel = "primaria"

                    },
                    new Nivel
                    {
                     Id = 2,
                    NombreNivel ="secundaria"

                    }
                 });

            _gradoFalse.Setup(o => o.GetGrados()).Returns(

                new List<Grado>
                {
                    new Grado
                    {
                        Id =1,
                        NivelId = 1,
                        NombreGrado ="primero"


                    },
                    new Grado
                    {
                        Id = 2,
                        NivelId =1,
                        NombreGrado = "segundo"
                    }
                });
            _seccionFalse.Setup(o => o.GetSecciones("")).Returns(

                new List<Seccion>
                {
                    new Seccion()
                    {
                        Id = 1,
                        NivelId = 1,
                        GradoId = 1,
                        NombreSeccion = "A",
                        Alumnos = GetHashCode()

                    },
                    new Seccion()
                    {
                        Id = 2,
                        NivelId = 1,
                        GradoId = 1,
                        NombreSeccion = "B",
                        Capasida = GetHashCode(),
                        Alumnos = GetHashCode()
                    }
                });

            //controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);


        }
 

        //[Test]
        //public void GuardarMatricula()
        //{
        //     var controller = new MatriculaController(_alumnoFalse.Object, _matriculaFalse.Object, _documentoFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _seccionFalse.Object, _apoderadoFalse.Object, _anioFalse.Object, _promedioFalse.Object, _ubigeoFalse.Object
        //    );

        //    controller.Create(new Matricula
        //    {
        //        Id = 1,
        //       SeccionId = 1,
        //       AlumnoId =1,
        //       AnioAcademicoId = GetHashCode(),
        //       ApoderadoId = GetHashCode(),
        //       FechaMatricula = DateTime.MaxValue,
        //       nombreAnio = ToString()
               
        //    });
        //    Assert.AreEqual(true, controller.ViewData.ModelState.IsValid);
        //}

        //[Test]
        //public void ListaMatriculas()
        //{
        //    _anioFalse.Setup(o => o.GetAniosAcademicos("")).Returns(
        //        new List<AnioAcademico>()
        //        {
        //            new AnioAcademico {
        //                Id =1,
        //                FechaInicioMatricula =DateTime.Now.AddMonths(-3),
        //                FechaInicio = DateTime.Now.AddMonths(-1),
        //                Descripcion = ToString(),
        //                FechaFin = DateTime.Now.AddMonths(+3),
        //                FechaMatriculaExtemporanea = DateTime.Now.AddMonths(-1),
        //                Anio = ToString(),
        //                FechaFinMatricula =DateTime.Now.AddMonths(-2)}
        //        }

        //    );
        //    _matriculaFalse.Setup(o => o.GetMatriculas("")).Returns(
        //        new List<Matricula>()
        //        {
        //            new Matricula {
        //                Id = 1,
        //                SeccionId = 1,
        //                AlumnoId =1,
        //                AnioAcademicoId =1,
        //                ApoderadoId = 1,
        //                FechaMatricula = DateTime.Now,
        //                nombreAnio = "WQW"
        //            }//new Matricula {
        //            //    Id = 2,
        //            //    SeccionId = 1,
        //            //    AlumnoId =2,
        //            //    AnioAcademicoId = 1,
        //            //    ApoderadoId = 1,
        //            //    FechaMatricula = DateTime.Now,
        //            //    nombreAnio = "WQW"}
        //        }
        //    );
        //    var controller = new MatriculaController(_alumnoFalse.Object, _matriculaFalse.Object, _documentoFalse.Object, _nivelFalse.Object, _gradoFalse.Object, _seccionFalse.Object, _apoderadoFalse.Object, _anioFalse.Object, _promedioFalse.Object, _ubigeoFalse.Object
        //    );
        //    controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
        //    var resultado = controller.Index() as PartialViewResult;

        //    Assert.AreEqual(2, (resultado.Model as List<Matricula>).Count());

        //}

    }
}
