using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using System.IO;

namespace JPCSystem.Web.Controllers
{
    public class AsistenciaController : Controller
    {
        private IAsistenciaService _service;
        private IMatriculaService _matricula;
        private ISeccionService _seccion;
        private IGradoService _grado;
        private INivelService _nivel;
        private ICursoService _curso;
        private IAnioEscolarService _anioEscolar;
        private IAlumnoService _alumno;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public AsistenciaController(IAsistenciaService service, IMatriculaService matricula,
            ISeccionService seccion, IGradoService grado,
            INivelService nivel, ICursoService curso,
            IAnioEscolarService anioEscolar,
            IAlumnoService alumno)
        {
            _service = service;
            _matricula = matricula;
            _seccion = seccion;
            _grado = grado;
            _nivel = nivel;
            _curso = curso;
            _anioEscolar = anioEscolar;
            _alumno = alumno;
        }

        // GET: Asistencia
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]


        public ActionResult Index()
        {
            // var lista = _service.GetAsistencias();
            ViewBag.nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Grado", Value = "0"}
            };
            var seccion = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Seccion", Value = "0"}
            };

            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");

            var actual = DateTime.Now;
            var consultaAnio = _anioEscolar.GetAniosAcademicos("").Where(
                a => actual >= a.FechaInicio && actual <= a.FechaFin
                );

            if (consultaAnio.Any())
            {
                return View(new ViewModelAsistencia {FechaAsistencia = DateTime.Now});
            }

            var consulta = _anioEscolar.GetAniosAcademicos("").First(
                a => a.Anio.Equals(actual.Year.ToString())
                );

            TempData["msj"] = "El Año escolar Inica el " + consulta.FechaInicio.ToString("D");

            return RedirectToAction("ListaAsistencias");

        }

        [HttpPost]
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]

    public ActionResult Index(ViewModelAsistencia asistencia)
        {
            ViewBag.nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };

            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");

            var anioScolar = _anioEscolar.GetAniosAcademicos("").Where(a => (a.FechaInicio <= asistencia.FechaAsistencia &&
                                                                              DateTime.Now.Year.Equals(asistencia.FechaAsistencia.Year))
                                                                            && (a.FechaFin.Year >= asistencia.FechaAsistencia.Year &&
                                                                            DateTime.Now.Year.Equals(asistencia.FechaAsistencia.Year)));

            var dia = DateTime.Now;
            var asistenciaExitente = _service.GetAsistencias("").Where(
                a => a.SeccionId.Equals(asistencia.SeccionId) && 
            a.FechaAsistencia.ToString("d").Equals(dia.ToString("d")));
            try
            {
                //validaAsistencia(asistencia);
                if (anioScolar.Any())
                {
                    if (asistencia.Alumnos != null)
                    {
                        if (!asistenciaExitente.Any())
                        {
                            if (asistencia.Alumnos.Count > 0)
                            {
                                var lista = new List<object>();
                                for (int i = 0; i < asistencia.Alumnos.Count; i++)
                                {
                                    var estadoA = asistencia.Alumnos.ToList()[i].Asis;
                                    if (string.IsNullOrEmpty(estadoA))
                                    {
                                        var alumno = _alumno.GetAlumno(asistencia.Alumnos.ToList()[i].IdAlumno);
                                        var nombre = alumno.Nombre + " " + alumno.ApPaterno + " " + alumno.ApMaterno;
                                        lista.Add(new {nombre});
                                        //aca esta la idea 
                                    }
                                }
                                if (lista.Count == 0)
                                {
                                    GuardarAsistencia(asistencia);
                                    return Json("ok");
                                }
                                return Json(lista);
                            }
                        }
                        TempData["msj"] = "Ya se llamo lista en esta Aula";
                        return Json("exiten");
                    }
                    TempData["msj"] = "Seleccione un Aula";
                    return Json("error");

                    //return RedirectToAction("ListaAsistencias");
                }
                return Json("ok");
            }
            catch (Exception e)
            {
                ViewBag.mensaje = "No existe alumnos reguistrados al Curso" + e;
            }

            return View(new ViewModelAsistencia {FechaAsistencia = DateTime.Now});
        }

        public void GuardarAsistencia(ViewModelAsistencia asistencia)
        {
            var anioScolar = _anioEscolar.GetAniosAcademicos("").Where(a => (a.FechaInicio <= asistencia.FechaAsistencia &&
                                                                             DateTime.Now.Year.Equals(asistencia.FechaAsistencia.Year))
                                                                            && (a.FechaFin.Year >= asistencia.FechaAsistencia.Year &&
                                                                                DateTime.Now.Year.Equals(asistencia.FechaAsistencia.Year)));

            for (int i = 0; i < asistencia.Alumnos.Count; i++)
            {
                asistencia.Alumnos.ToList()[i].Asistio =
                    asistencia.Alumnos.ToList()[i].Asis.ToUpper() == "F" ? false : true;

                var asistencias = new Asistencia()
                {
                    AnioAcademicoId = anioScolar.First().Id,
                    Estado = asistencia.Alumnos.ToList()[i].Asistio,
                    FechaAsistencia = asistencia.FechaAsistencia,
                    SeccionId = asistencia.SeccionId,
                    AlumnoId = asistencia.Alumnos.ToList()[i].IdAlumno
                };
                _service.AddAsistencia(asistencias);
            }

        }


        public ActionResult ListaAsistencias()
        {
            ViewBag.nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };

            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");

            var lista = _service.GetAsistencias("");

            ViewData["falta"]= TempData["falta"];
            ViewData["asiste"] = TempData["asiste"];

            return View(new List<Asistencia>());
        }

        [HttpPost]
        public ActionResult ListaAsistencias(string criterio,int? idNivel,int? gradoId, int? seccionId)
        {
            var idN = idNivel ?? 0;
            var idG = gradoId ?? 0;
            var idS = seccionId ?? 0;
            ViewBag.nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };

            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");
            var actual = DateTime.Now;
            var lista = new List<Asistencia>();
            if (!string.IsNullOrEmpty(criterio) && idN == 0 && idG == 0 && idS == 0)
            {
                lista = _service.GetAsistencias(criterio).AsQueryable().Include("AnioAcademico").Where(
                    a=>a.AnioAcademico.Anio.Equals(actual.Year.ToString())).ToList();
            }
            if (!string.IsNullOrEmpty(criterio) && idN > 0 && idG > 0 && idS > 0)
            {
                lista = _service.GetAsistencias(criterio).AsQueryable().AsQueryable().Include("AnioAcademico").Include("Seccion").Include("Seccion.Grado").
                    Include("Seccion.Grado.Nivel").Include("Alumno").Where(
                    a=>a.SeccionId.Equals(idS) && a.Seccion.GradoId.Equals(idG)
                    && a.Seccion.Grado.NivelId.Equals(idN) && a.AnioAcademico.Anio.Equals(actual.Year.ToString())
                ).ToList();
            }

            if (string.IsNullOrEmpty(criterio) && idN > 0 && idG > 0 && idS > 0)
            {
                //var dateTime = actual.ToString("d");
                lista = _service.GetAsistencias("").AsQueryable().Include("Seccion").Include("Seccion.Grado").
                    Include("Seccion.Grado.Nivel").Include("Alumno").Include("AnioAcademico").Where(
                    a => a.SeccionId.Equals(idS) && a.Seccion.GradoId.Equals(idG)
                         && a.Seccion.Grado.NivelId.Equals(idN) && 
                         a.FechaAsistencia.Day.Equals(actual.Day) && a.FechaAsistencia.Month.Equals(actual.Month)
                         && a.FechaAsistencia.Year.Equals(actual.Year)

                ).ToList();
            }

           
            var data = new
            {
                asiste = lista.Count(c => c.Estado.Equals(false)),
                falta = lista.Count(c => c.Estado.Equals(true))
            };

            return Json(new { info = PartialView(this, "_ListaAistencia", lista), msj = data });
        }

        public String ConvertString(DateTime fecha)
        {
            var convertFecha = fecha.ToString("d");
            return convertFecha;
        }

        public static string PartialView(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }

        // GET: Asistencia/Create
        public ActionResult Create()
        {
            ViewBag.curso = _curso.GetCursos("");
            ViewBag.nivel = _nivel.GetNiveles();
            ViewBag.grado = _grado.GetGrados();
            ViewBag.seccion = _seccion.GetSecciones("");
            return View();
        }

        // POST: Asistencia/Create
        [HttpPost]
        public ActionResult Create(Asistencia asistencia)
        {
            try
            {
                ViewBag.curso = _curso.GetCursos("");
                ViewBag.nivel = _nivel.GetNiveles();
                ViewBag.grado = _grado.GetGrados();
                ViewBag.seccion = _seccion.GetSecciones("");

                if (ModelState.IsValid)
                {
                    _service.AddAsistencia(asistencia);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asistencia/Edit/5
       // [Authorize(Roles = "admin,docente")]
       [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var editAlumno = _service.GetAsistencia(id);
            var actual = DateTime.Now.ToString("d");

            if (editAlumno.FechaAsistencia.ToString("d") == actual)
            {
                var alumno = _alumno.GetAlumno(editAlumno.AlumnoId);
                var alumnoModel = new AsistemciAlumnoModel
                {
                    AlumnoId = editAlumno.AlumnoId,
                    Fecha = editAlumno.FechaAsistencia,
                    Asistencia = editAlumno.Estado,
                    AsistenciId = editAlumno.Id,
                    seccionId = editAlumno.SeccionId,
                    Nombre = alumno.NombreCompleto,
                    AnioAcademicoId = editAlumno.AnioAcademicoId
                };

                return PartialView("_Edit", alumnoModel);
            }
            return Json("error",JsonRequestBehavior.AllowGet);
        }

        // POST: Asistencia/Edit/5
        [HttpPost]
       // [Authorize(Roles = "admin,docente")]
       [AllowAnonymous]
        public ActionResult EditAsistencia(AsistemciAlumnoModel asistencia)
        {
            try
            {
                var asiste = _service.GetAsistencia(asistencia.AsistenciId);

                asiste.Estado = asistencia.estado == "on" || asistencia.estado == "True" ? true :false ;
                _service.UpdateAsistencia(asiste);

                var persona = _service.GetAsistencias("").Where(a => a.SeccionId.Equals(asistencia.seccionId));
                return PartialView("_ListaAistencia",persona);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Asistencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asistencia/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetNivel(int nivelId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var grado = _grado.GetGrados().Where(g => g.NivelId == nivelId);
            return Json(grado);
        }

        public JsonResult GetSeccion(int gradoId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var seccion = _seccion.GetSecciones("").Where(s => s.GradoId == gradoId);
            //var seccion = _seccion.GetSecciones("");
            return Json(seccion);
        }

        public JsonResult GetCurso(int gradoId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var curso = _curso.GetCursos("").Where(c => c.GradoId == gradoId);
            return Json(curso);
        }

        public ActionResult GetAlumnos(int seccionId)
        {
            var alumnos = _matricula.GetAlumnos(seccionId);

            var asistenciaModel = new ViewModelAsistencia();

            var asistencia = from a in alumnos
                             select new AlumnoModelAsiste()
                             {
                                 IdAlumno = a.Id,
                                 Alumno = a.ApPaterno + " " + a.ApMaterno + " " + a.Nombre,
                                 Asistio = false
                             };

            asistenciaModel.Alumnos = asistencia.ToList();

            return PartialView(asistenciaModel);
        }

        public ActionResult GetReposteAsistenciaAlumnos()
        {
            ViewBag.Nivel = _nivel.GetNiveles();
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };
            var actual = DateTime.Now.Year.ToString();
            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");
            ViewBag.anio = new SelectList(_anioEscolar.GetAniosAcademicos("").Where(a=>int.Parse(a.Anio)<= int.Parse(actual)), "Id", "Anio");

            


            return PartialView("_ReposteAsistencia",new Repostes());
        }

        [HttpPost]
        public ActionResult GetReposteAsistenciaAlumnos(int? seccionId, int? anioId, string criterio)
        {
            var idSec = seccionId ?? 0;
            var idAnio = anioId ?? 0;
           
            var reporte = new Repostes();
            if (idSec==0 && idAnio >0 && string.IsNullOrEmpty(criterio))
            {
                var anioAca = _anioEscolar.GetAnioAcademico(idAnio).Id;
                var cantMatricula = _matricula.GetMatriculas("").Count(a=>a.AnioAcademicoId.Equals(anioAca));
                var totalA = _service.GetAsistencias("").Count(
                    a => a.AnioAcademicoId.Equals(anioAca) && a.Estado.Equals(true));
                var totalF = _service.GetAsistencias("").Count(
                    a => a.AnioAcademicoId.Equals(anioAca) && a.Estado.Equals(false));
                reporte = Repostes(cantMatricula, totalA, totalF);
                
            }

            if (idSec == 0 && idAnio > 0 && !string.IsNullOrEmpty(criterio))
            {
                var alu = _alumno.GetAlumnos("").Where(a => a.NombreCompleto.ToUpper().Equals(criterio.ToUpper()));

                var enumerable = alu as Alumno[] ?? alu.ToArray();
                if (enumerable.Any())
                {
                    var alumno = enumerable.First();
                    var cantMatricula = _matricula.GetMatriculas("").Count(a => a.AnioAcademicoId.Equals(anioId) &&
                                                                                a.AlumnoId.Equals(alumno.Id)
                    );
                    var totalA = _service.GetAsistencias("").Count(
                        a => a.AnioAcademicoId.Equals(anioId)
                             && a.Estado.Equals(true)
                             && a.AlumnoId.Equals(alumno.Id));
                    var totalF = _service.GetAsistencias("").Count(
                        a => a.AnioAcademicoId.Equals(anioId)
                             && a.Estado.Equals(false)
                             && a.AlumnoId.Equals(alumno.Id));
                    reporte = Repostes(cantMatricula, totalA, totalF);
                }
                else
                {
                    reporte  = Repostes(0, 0, 0);
                }

            }

            if (idSec > 0 && idAnio == 0 && string.IsNullOrEmpty(criterio))
            {
                var actual = DateTime.Now.Year.ToString();
                var cantMatricula = from a in _matricula.GetMatriculas("").AsQueryable().Include("AnioAcademico")
                    where a.AnioAcademico.Anio.Equals(actual) 
                    && a.SeccionId == seccionId
                    select a;
                //                    .Count(a => a.AnioAcademico.Anio.Equals(actual)
                //&& a.SeccionId.Equals(seccionId)
                //);
                var totalA = from a in _service.GetAsistencias("").AsQueryable().Include("AnioAcademico")
                    where a.AnioAcademico.Anio.Equals(actual) && a.SeccionId == seccionId && a.Estado.Equals(true)
                    select a;
                var totalF = from a in _service.GetAsistencias("").AsQueryable().Include("AnioAcademico")
                    where a.AnioAcademico.Anio.Equals(actual) 
                    && a.SeccionId== seccionId 
                    && a.Estado.Equals(false)
                    select a;
                reporte = Repostes(cantMatricula.Count(), totalA.Count(), totalF.Count());
               
            }

            if (idSec > 0 && idAnio == 0 && !string.IsNullOrEmpty(criterio))
            {
                var alu = _alumno.GetAlumnos("").Where(a => a.NombreCompleto.ToUpper().Equals(criterio.ToUpper()));

                var enumerable = alu as Alumno[] ?? alu.ToArray();
                if (enumerable.Any())
                {
                    var alumno = enumerable.First();
                    var cantMatricula = _matricula.GetMatriculas("").Count(a => a.SeccionId.Equals(seccionId) &&
                                                                                a.AlumnoId.Equals(alumno.Id)
                    );
                    var totalA = _service.GetAsistencias("").Count(
                        a =>a.SeccionId.Equals(seccionId)
                             && a.Estado.Equals(true)
                             && a.AlumnoId.Equals(alumno.Id));
                    var totalF = _service.GetAsistencias("").Count(
                        a => a.SeccionId.Equals(seccionId)
                             && a.Estado.Equals(false)
                             && a.AlumnoId.Equals(alumno.Id));
                    reporte = Repostes(cantMatricula, totalA, totalF);
                }
                else
                {
                    reporte = Repostes(0, 0, 0);
                }

            }


            if (idSec > 0 && idAnio > 0 && string.IsNullOrEmpty(criterio))
            {
                var cantMatricula = _matricula.GetMatriculas("").Count(a => a.AnioAcademicoId.Equals(anioId)
                                                                            && a.SeccionId.Equals(seccionId)
                );
                var totalA = _service.GetAsistencias("").Count(
                    a => a.AnioAcademicoId.Equals(anioId) 
                    && a.SeccionId.Equals(seccionId) 
                    && a.Estado.Equals(true));
                var totalF = _service.GetAsistencias("").Count(
                    a => a.AnioAcademicoId.Equals(anioId) 
                    && a.SeccionId.Equals(seccionId) 
                    && a.Estado.Equals(false));
                reporte = Repostes(cantMatricula, totalA, totalF);

            }

            if (idSec > 0 && idAnio > 0 && !string.IsNullOrEmpty(criterio))
            {
                var alu = _alumno.GetAlumnos("").Where(a => a.NombreCompleto.ToUpper().Equals(criterio.ToUpper()));

                var enumerable = alu as Alumno[] ?? alu.ToArray();
                if (enumerable.Any())
                {
                    var alumno = enumerable.First();
                    var cantMatricula = _matricula.GetMatriculas("").Count(a => a.AnioAcademicoId.Equals(anioId)
                                                                                && a.SeccionId.Equals(seccionId) && 
                                                                                a.AlumnoId.Equals(alumno.Id)
                    );
                    var totalA = _service.GetAsistencias("").Count(
                        a => a.AnioAcademicoId.Equals(anioId) 
                        && a.SeccionId.Equals(seccionId) 
                        && a.Estado.Equals(true)
                        && a.AlumnoId.Equals(alumno.Id));
                    var totalF = _service.GetAsistencias("").Count(
                        a => a.AnioAcademicoId.Equals(anioId) 
                        && a.SeccionId.Equals(seccionId) 
                        && a.Estado.Equals(false) 
                        && a.AlumnoId.Equals(alumno.Id));
                    reporte = Repostes(cantMatricula, totalA, totalF);
                }
                else
                {
                    reporte = Repostes(0, 0, 0);
                }
            }

            if (idSec == 0 && idAnio == 0 && !string.IsNullOrEmpty(criterio))
            {
                var alu = _alumno.GetAlumnos("").Where(a => a.NombreCompleto.ToUpper().Equals(criterio.ToUpper()));

                var enumerable = alu as Alumno[] ?? alu.ToArray();
                if (enumerable.Any())
                {
                    var alumno = enumerable.First();
                    var cantMatricula = _matricula.GetMatriculas("").Count(a => a.AlumnoId.Equals(alumno.Id));
                    var totalA = _service.GetAsistencias("").Count(a => a.Estado.Equals(true) 
                    && a.AlumnoId.Equals(alumno.Id));
                    var totalF = _service.GetAsistencias("").Count(a => a.Estado.Equals(false) 
                    && a.AlumnoId.Equals(alumno.Id));
                    reporte = Repostes(cantMatricula, totalA, totalF);
                }
                else
                {
                    reporte = Repostes(0, 0, 0);
                }
            }

            return Json(new { info = PartialView(this, "ReporteLista", reporte), msj = "ok" });
        }

        public Repostes Repostes(int total, int a, int f)
        {
            var reporte = new Repostes
            {
                TotalAlumnos = total,
                TotalA = a,
                TotalD = f,
            };

            return reporte;
        }
    }
}