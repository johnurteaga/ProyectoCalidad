using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Repository;
using JPCSystem.Service;
using JPCSystem.Web.Models;

namespace JPCSystem.Web.Controllers
{
    public class NotasController : Controller
    {
        private INotasService _service;
        private IMatriculaService _matricula;
        private ISeccionService _seccion;
        private IGradoService _grado;
        private INivelService _nivel;
        private ICursoService _curso;
        private ITrimestreService _trimestre;
        private IAnioEscolarService _anio;
        private IRegNotasService _registro;
        private IPromedioService _promedio;
        private IAlumnoService _alumno;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public NotasController(INotasService service, IMatriculaService matricula, 
            ISeccionService seccion,
            IGradoService grado, INivelService nivel, 
            ICursoService curso, ITrimestreService trimestre,
            IAnioEscolarService anio, IRegNotasService registro,
            IPromedioService promedio, IAlumnoService alumno)
        {
            _service = service;
            _matricula = matricula;
            _seccion = seccion;
            _grado = grado;
            _nivel = nivel;
            _curso = curso;
            _trimestre = trimestre;
            _anio = anio;
            _registro = registro;
            _promedio = promedio;
            _alumno = alumno;
        }

        // GET: Notas
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var actual = DateTime.Now;
            var anio = _registro.GetRegistrosNotas("")
                .First(x => x.FechaInicioPrimTrim <= actual && actual <= x.FechaFinPrimTrim);

            ViewBag.trimestre = _trimestre.GeTrimestres().First(t => t.Id.Equals(anio.TrimestreId)).Descripcion;
            ViewBag.nivel = _nivel.GetNiveles();
            ViewBag.grado = _grado.GetGrados();
            ViewBag.seccion = _seccion.GetSecciones("");

            ViewBag.nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };
            var curso = new List<SelectListItem>() {
                new SelectListItem() { Text = "Curso", Value = "0" }
            };
            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");
            ViewBag.curso = new SelectList(curso, "Value", "Text");

            return View("IndexCreate", new NotasViewModel
            {
                TrimestreId = anio.TrimestreId
            });
        }

        [HttpPost]
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]
        public ActionResult Index(NotasViewModel model)
        {
            ViewBag.grado = new SelectList(_grado.GetGrados().Where(g=>g.NivelId.Equals(model.NivelId)), "Id", "NombreGrado");
            ViewBag.seccion = new SelectList(_seccion.GetSecciones("").Where(s=>s.GradoId.Equals(model.GradoId)), "Id", "NombreSeccion");
            ViewBag.curso = new SelectList(_curso.GetCursos("").Where(c=>c.GradoId.Equals(model.GradoId)), "Id", "NombreCurso");


            ViewBag.trimestre = _trimestre.GeTrimestres().First(t => t.Id.Equals(model.TrimestreId)).Descripcion;
            ViewBag.nivel = _nivel.GetNiveles();


            if (model.Alumnos != null)
            {
                for (int i = 0; i < model.Alumnos.Count; i++)
                {
                    var notaAlumnos = (model.Alumnos.ToList()[i].Nota1 + model.Alumnos.ToList()[i].Nota2 +
                                       model.Alumnos.ToList()[i].Nota3 + model.Alumnos.ToList()[i].Nota4) / 4;
                    model.Alumnos.ToList()[i].Vigecimal = notaAlumnos;

                    if (notaAlumnos >= 0 && notaAlumnos <= 11)
                    {
                        model.Alumnos.ToList()[i].Cualitativa = "C";
                    }
                    if (notaAlumnos > 11 && notaAlumnos <= 15)
                    {
                        model.Alumnos.ToList()[i].Cualitativa = "B";
                    }
                    if (notaAlumnos > 15 && notaAlumnos <= 18)
                    {
                        model.Alumnos.ToList()[i].Cualitativa = "A";
                    }
                    if (notaAlumnos > 18 && notaAlumnos <= 20)
                    {
                        model.Alumnos.ToList()[i].Cualitativa = "AD";
                    }

                    

                    var nota = new Nota()
                    {
                        FechaRegistro = DateTime.Now,
                        AlumnoId = model.Alumnos.ToList()[i].IdAlumno,
                        Cualitativa = model.Alumnos.ToList()[i].Cualitativa,
                        CursoId = model.CursoId,
                        GradoId = model.GradoId,
                        SeccionId = model.SeccionId,
                        NivelId = model.NivelId,
                        Nota1 = model.Alumnos.ToList()[i].Nota1,
                        Nota2 = model.Alumnos.ToList()[i].Nota2,
                        Nota3 = model.Alumnos.ToList()[i].Nota3,
                        Nota4 = model.Alumnos.ToList()[i].Nota4,
                        TrimestreId = model.TrimestreId,
                        Vigecimal = model.Alumnos.ToList()[i].Vigecimal
                    };

                    _service.AddNota(nota);
                    int nota1 = 0;
                    if (nota.TrimestreId == 1)
                    {
                        nota1 = nota.Nota1;
                    }


                    //var propmedio = new Promedio()
                    //{
                    //    NotaId = nota.Id,
                    //    PrimerTrimPromedio = (nota.Nota1 + nota.Nota2 + nota.Nota3 + nota.Nota4)/4,
                        
                    //}
                }
            }

        return View("IndexCreate");
        }
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]
        public ActionResult ListaNotas()
        {
            var actual = DateTime.Now;
            var anio = _registro.GetRegistrosNotas("")
                .First(x => x.FechaInicioPrimTrim <= actual && actual <= x.FechaFinPrimTrim);

            ViewBag.trimestre = new SelectList(_trimestre.GeTrimestres().Where(t => t.Id.Equals(anio.TrimestreId)), "Id", "Descripcion");

            ViewBag.nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };
            var curso = new List<SelectListItem>() {
                new SelectListItem() { Text = "Curso", Value = "0" }
            };
            ViewBag.anio = new SelectList(_anio.GetAniosAcademicos(""), "Id", "Anio");

            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");
            ViewBag.curso = new SelectList(curso, "Value", "Text");
            ViewBag.trimestre = new SelectList(_trimestre.GeTrimestres(), "Id", "Descripcion");

            return View("ListaNotas",new List<PromedioViewModel>());
        }

        public ActionResult GetAlumnos(int seccionId)
        {
            var alumnos = _matricula.GetAlumnos(seccionId);

            var notasModel = new NotasViewModel();

            var nota = from a in alumnos
                       select new AlumnosModel()
                       {
                           IdAlumno = a.Id,
                           Alumno = a.ApPaterno + " " + a.ApMaterno + " " + a.Nombre,
                           Cualitativa = " ",
                           Nota1 = 0,
                           Nota2 = 0,
                           Nota3 = 0,
                           Nota4 = 0,
                           Vigecimal = 0
                       };

            notasModel.Alumnos = nota.ToList();

            return PartialView(notasModel);
        }

        public JsonResult GetGrado(int nivelId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var grado = _grado.GetGrados().Where(g => g.NivelId == nivelId);
            return Json(grado);
        }

        public JsonResult GetSeccionAndGrado(int gradoId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            //var seccion = _seccion.GetSecciones("");
            var seccion = _seccion.GetSecciones("").Where(s => s.GradoId == gradoId);
            var curso = _curso.GetCursos("").Where(c => c.GradoId == gradoId);
            var data = new
            {
                s = seccion,
                c = curso
            };
            return Json(data);
        }
       
    }
}