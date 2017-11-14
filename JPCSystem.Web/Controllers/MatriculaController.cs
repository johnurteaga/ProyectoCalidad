using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class MatriculaController : Controller
    {
        private IAlumnoService _AlumnoService;
        private IMatriculaService _service;
        private IDocumentoService _DocumentoService;
        private INivelService _NivelService;
        private IGradoService _gradoService;
        private ISeccionService _seccionService;
        private IApoderadoService _apoderado;
        private IAnioEscolarService _anio;
        private IPromedioService _promedio;
        private IUbigeoService _ubigeo;
        private readonly ValidacionesUsuario _validaciones;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public MatriculaController(IAlumnoService alumnoService, IMatriculaService service,
            IDocumentoService documentoService, 
            INivelService nivelService,
            IGradoService gradoService, 
            ISeccionService seccionService,
            IApoderadoService apoderado,
            IAnioEscolarService anio,
            IPromedioService promedio,
            IUbigeoService ubigeo)
        {
            _AlumnoService = alumnoService;
            _service = service;
            _DocumentoService = documentoService;
            _NivelService = nivelService;
            _gradoService = gradoService;
            _seccionService = seccionService;
            _apoderado = apoderado;
            _anio = anio;
            _promedio = promedio;
            _ubigeo = ubigeo;
            _validaciones =new ValidacionesUsuario(ModelState);
        }

        // GET: Matricula
        public ActionResult Index()
        {
            ViewBag.alumno = new SelectList(new List<SelectListItem>
            {
                new SelectListItem() { Text = "Seleccione", Value = "0" }
            }, "Value", "Text");
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };
            ViewBag.anios = new SelectList(_anio.GetAniosAcademicos(""), "Id", "Anio");

            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");
            ViewBag.nivel = new SelectList(_NivelService.GetNiveles(), "Id", "NombreNivel");
            var fecha = DateTime.Now.Year.ToString();
            var lista = _service.GetMatriculas("").Where(d=>d.AnioAcademico.Anio.Equals(fecha));
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(String criterio,int? nivelId,int? gradoId,int? seccionId,int? anioId)
        {
            var nivel = (nivelId ?? 0);
            var grado = (gradoId ?? 0);
            var seccion = (seccionId ?? 0);
            var anio = (anioId ?? 0);

            var actual = DateTime.Now.Year.ToString();
            var lista = _service.GetMatriculas("").Where(m=>m.AnioAcademico.Anio.Equals(actual));
            if (!string.IsNullOrEmpty(criterio) && nivel == 0 && grado == 0 && seccion == 0 && anio == 0)
            {
                lista = _service.GetMatriculas(criterio);
            }
            if (string.IsNullOrEmpty(criterio) && nivel > 0 && grado == 0 && seccion == 0 && anio == 0)
            {
                lista = from a in lista.AsQueryable()
                        where a.Seccion.Grado.NivelId.Equals(nivel)
                        select a;
            }

            if (string.IsNullOrEmpty(criterio) && nivel > 0 && grado > 0 && seccion == 0 && anio == 0)
            {
                lista = from a in lista.AsQueryable()
                    where a.Seccion.Grado.NivelId.Equals(nivel) &&
                          a.Seccion.GradoId.Equals(grado)
                        select a;
            }

            if (string.IsNullOrEmpty(criterio) && nivel > 0 && grado > 0 && seccion > 0 && anio == 0)
            {
                lista = from a in lista.AsQueryable()
                    where a.Seccion.Grado.NivelId.Equals(nivel) &&
                          a.Seccion.GradoId.Equals(grado) && 
                          a.SeccionId.Equals(seccion)
                        select a;
            }
            if (string.IsNullOrEmpty(criterio) && nivel > 0 && grado > 0 && seccion > 0 && anio > 0)
            {
                lista = from a in lista.AsQueryable()
                    where a.Seccion.Grado.NivelId.Equals(nivel) &&
                          a.Seccion.GradoId.Equals(grado) && 
                          a.SeccionId.Equals(seccion) &&
                          a.AnioAcademicoId.Equals(anio)
                    select a;
            }

            if (!string.IsNullOrEmpty(criterio) && nivel > 0 && grado > 0 && seccion > 0 && anio > 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                    where a.Seccion.Grado.NivelId.Equals(nivel) &&
                          a.Seccion.GradoId.Equals(grado) &&
                          a.SeccionId.Equals(seccion) &&
                          a.AnioAcademicoId.Equals(anio)
                    select a;
            }

            if (!string.IsNullOrEmpty(criterio) && nivel > 0 && grado > 0 && seccion > 0 && anio == 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                    where a.Seccion.Grado.NivelId.Equals(nivel) &&
                          a.Seccion.GradoId.Equals(grado) &&
                          a.SeccionId.Equals(seccion) &&
                          a.AnioAcademicoId.Equals(anio)
                    select a;
            }

            if (!string.IsNullOrEmpty(criterio) && nivel > 0 && grado == 0 && seccion == 0 && anio == 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                    where a.Seccion.Grado.NivelId.Equals(nivel)
                    select a;
            }


            if (string.IsNullOrEmpty(criterio) && nivel == 0 && grado > 0 && seccion == 0 && anio == 0)
            {
                lista = from a in lista.AsQueryable()
                        where a.Seccion.Grado.Id.Equals(grado)
                        select a;
            }
            if (!string.IsNullOrEmpty(criterio) && nivel == 0 && grado > 0 && seccion == 0 && anio == 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                    where a.Seccion.Grado.Id.Equals(grado)
                    select a;
            }
            if (string.IsNullOrEmpty(criterio) && nivel == 0 && grado == 0 && seccion > 0 && anio == 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                        where a.SeccionId.Equals(seccion)
                        select a;
            }
            if (!string.IsNullOrEmpty(criterio) && nivel == 0 && grado == 0 && seccion > 0 && anio == 0)
            {
                lista = from a in lista.AsQueryable()
                    where a.SeccionId.Equals(seccion)
                    select a;
            }
            if (string.IsNullOrEmpty(criterio) && nivel == 0 && grado == 0 && seccion > 0 && anio == 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                    where a.SeccionId.Equals(seccion)
                    select a;
            }
            if (!string.IsNullOrEmpty(criterio) && nivel == 0 && grado == 0 && seccion == 0 && anio > 0)
            {
                lista = from a in lista.AsQueryable()
                        where a.AnioAcademicoId.Equals(anio)
                        select a;
            }

            if (string.IsNullOrEmpty(criterio) && nivel == 0 && grado == 0 && seccion == 0 && anio > 0)
            {
                lista = from a in _service.GetMatriculas(criterio).AsQueryable()
                    where a.AnioAcademicoId.Equals(anio)
                    select a;
            }

            return PartialView("_ListaMatricula",lista);
        }

        // GET: Matricula/Details/5
        public ActionResult Details(int id)
        {
            var lista = _service.GetMatricula(id);
            var alumno = _AlumnoService.GetAlumno(lista.AlumnoId);
            ViewBag.al = alumno.Nombre + " " + alumno.ApPaterno + " " + alumno.ApMaterno;
            var ap = _apoderado.GetApoderado(lista.ApoderadoId);
            ViewBag.apoderado = ap.Nombres + " " + ap.ApPaterno + " " + ap.ApMaterno;
            ViewBag.nivel = _NivelService.GetNivel(lista.Seccion.Grado.NivelId).NombreNivel;
            ViewBag.grado = _gradoService.GetGrado(lista.Seccion.GradoId).NombreGrado;
            ViewBag.seccion = _seccionService.GetSeccion(lista.SeccionId).NombreSeccion;
            ViewBag.anio = _anio.GetAnioAcademico(lista.AnioAcademicoId).Descripcion;

            return View(lista);
        }

        // GET: Matricula/Create
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.anios = new SelectList(_anio.GetAniosAcademicos(""), "Id", "Anio");
            ViewBag.alumno = new SelectList(new List<SelectListItem>
            {
                new SelectListItem() { Text = "Seleccione", Value = "0" }
            }, "Value", "Text");
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Grado", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seccion", Value = "0" }
            };
            ViewBag.grado = new SelectList(grados, "Value", "Text");
            ViewBag.nivel = new SelectList(_NivelService.GetNiveles(), "Id", "NombreNivel");
            ViewBag.seccion = new SelectList(seccion, "Value", "Text");

            var fecha = DateTime.Now;
            var anio = _anio.GetAniosAcademicos("").
                Where(a => a.Anio.Equals(fecha.Year.ToString()));
            if (anio.Any())
            {
               
                if (anio.Any(a=>a.FechaInicioMatricula <= fecha && fecha <= a.FechaFinMatricula || fecha == a.FechaMatriculaExtemporanea))
                {
                    SelectList(0);
                    return View("Create", new Matricula
                    {
                        FechaMatricula = DateTime.Now,
                        nombreAnio = anio.First().Anio,
                        AnioAcademicoId = anio.First().Id

                    });
                }
                TempData["mensaje"] = "El periodo de matricula a concluido";
                return View("Index", _service.GetMatriculas(""));
            }
            
            TempData["mensaje"] = "No se ha configurado ningun año escolar.";
            return View("Index",_service.GetMatriculas(""));

        }

        public void SelectList(int estado)
        {

            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione", Value = "0" }
            };
            var seccion = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione", Value = "0" }
            };

            if (estado == 0)
            {
                ViewBag.grados = new SelectList(grados, "Value", "Text");
                ViewBag.niveles = new SelectList(_NivelService.GetNiveles(), "Id", "NombreNivel");
                ViewBag.seccion = new SelectList(seccion, "Value", "Text");
                ViewBag.documento = _DocumentoService.GetDocumentos();
            }
            else
            {
                var se = _seccionService.GetSecciones("").Where(s => s.Id.Equals(estado));
                var gd = _gradoService.GetGrados().Where(g => g.Id.Equals(se.First().GradoId));
                var ni = _NivelService.GetNiveles().Where(n => n.Id.Equals(gd.First().NivelId));
                var alumno = _AlumnoService.GetAlumnos("").Where(a => a.Id.Equals(estado));

                ViewBag.grados = new SelectList(gd, "Id", "NombreGrado");
                ViewBag.niveles = new SelectList(ni, "Id", "NombreNivel");
                ViewBag.seccion = new SelectList(se, "Id", "NombreSeccion");
                ViewBag.documento = _DocumentoService.GetDocumentos();
            }
        }

        // POST: Matricula/Create
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Create(Matricula matricula)
        {
            try
            {
                SelectList(matricula.SeccionId);
                Validaciones(matricula);
                var fecha = DateTime.Now;
                var lista = _service.GetMatriculas("").Where(d => d.AnioAcademico.Anio.Equals(fecha.Year.ToString()));
                if (ModelState.IsValid)
                {
                    if (matricula.Id == 0)
                    {
                        if (!ExisteAlmnoMatriculado(matricula))
                        {
                            matricula.FechaMatricula = DateTime.Now;
                            var alumno = _AlumnoService.GetAlumno(matricula.AlumnoId);
                            alumno.Estado = false;
                            _AlumnoService.UpdateAlumno(alumno);

                            _service.AddMatricula(matricula);
                            TempData["mensaje"] = "Matricula realizada con exito.";
                            return RedirectToAction("Index");
                        }
                        ViewData["mensaje"] = "La matricula ya ha sido registrada anteriormente.";
                        //TempData["existe"] = "La matricula ya ha sido registrada anteriormente.";
                        return View("Create", matricula);

                    }
                    if (matricula.Id > 0)
                    {
                        _service.UpdateMatricula(matricula);
                        return View("Index", lista);
                    }
                }
                matricula.FechaMatricula = DateTime.Now;

                var anio = _anio.GetAniosAcademicos("").Where(a => a.Anio.Equals(fecha.Year.ToString()));
                matricula.nombreAnio = anio.Max().Anio;
                return View("Create",matricula);
            }
            catch (Exception ex)
            {

                return PartialView("Create",matricula);
            }
        }

        private bool ExisteAlmnoMatriculado(Matricula matricula)
        {
            var fecha = DateTime.Now.Year.ToString();
            var consulta = from a in _service.GetMatriculas("").AsQueryable().Include("Alumno").Include("AnioAcademico")
                where a.AlumnoId.Equals(matricula.AlumnoId) && a.AnioAcademicoId.Equals(matricula.AnioAcademicoId) 
                        && a.AnioAcademico.Anio.Equals(fecha)
                select a;

            return consulta.Any();
        }

        // GET: Matricula/Edit/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            //ViewBag.documento = _DocumentoService.GetDocumentos();
            //ViewBag.Niveles = _NivelService.GetNiveles();
            //ViewBag.grado = _gradoService.GetGrados();
            //ViewBag.seccion = _seccionService.GetSecciones("");
            //ViewBag.alumn = _AlumnoService.GetAlumnos("");
            //ViewBag.apoderado = _apoderado.GetApoderados("");
            //ViewBag.anio = _anio.GetAniosAcademicos("");

            
            var matriculado = _service.GetMatricula(id);
            var seccion = _seccionService.GetSeccion(matriculado.SeccionId);
            var grado = _gradoService.GetGrado(seccion.GradoId);
            var nivel = _NivelService.GetNivel(grado.NivelId);
            var apoderado = _apoderado.GetApoderado(matriculado.ApoderadoId);
            var alumno = _AlumnoService.GetAlumno(matriculado.AlumnoId);
            var anio = _anio.GetAnioAcademico(matriculado.AnioAcademicoId);

            var matriculados = _service.GetMatriculas("").Count(m => m.SeccionId.Equals(seccion.Id) && m.AnioAcademicoId.Equals(anio.Id));

            SelectList(matriculado.SeccionId);

            return View(new MatriculaModel
            {
                Id=matriculado.Id,
                anio = anio.Anio,
                AlumnoId = alumno.Id,
                ApoderadoId = apoderado.Id,
                AnioAcademicoId = matriculado.AnioAcademicoId,
                FechaDateTime = matriculado.FechaMatricula,
                NombreAlumno = alumno.Nombre +" " + alumno.ApPaterno + " " + alumno.ApMaterno,
                Nombreapoderado = apoderado.Nombres + " "+ apoderado.ApPaterno + " " + apoderado.ApMaterno,
                niveId = nivel.Id,
                gradoId = grado.Id,
                seccinId = seccion.Id,
                nroDocApoderado = apoderado.NroDocumento,
                nroDocAlumno = alumno.NumeroDocumento,
                tipoDocAlumnoId = alumno.DocumentoId,
                tipoDocApoderadoId = apoderado.DocumentoId,
                vacantes = (seccion.Capasida??0)-matriculados,
                matriculados = matriculados

            
                
            });
        }

        // POST: Matricula/Edit/5
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Edit(MatriculaModel matricula)
        {
            try
            {
                ViewBag.documento = _DocumentoService.GetDocumentos();
                ViewBag.Nivel = _NivelService.GetNiveles();
                ViewBag.grado = _gradoService.GetGrados();
                ViewBag.seccion = _seccionService.GetSecciones("");
                ViewBag.alumn = _AlumnoService.GetAlumnos("");
                ViewBag.apoderado = _apoderado.GetApoderados("");
                ViewBag.anio = _anio.GetAniosAcademicos("");
                var matriculaNueva = new Matricula
                {
                    Id = matricula.Id,
                    AlumnoId = matricula.AlumnoId,
                    AnioAcademicoId = matricula.AnioAcademicoId,
                    ApoderadoId = matricula.ApoderadoId,
                    FechaMatricula = matricula.FechaDateTime,
                    SeccionId = matricula.seccinId
                };

                Validaciones(matriculaNueva);
                if (ModelState.IsValid)
                {
                    _service.UpdateMatricula(matriculaNueva);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Matricula/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_service.GetMatricula(id));
        }

        // POST: Matricula/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Matricula matricula)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    matricula = _service.GetMatricula(id);

                    if (matricula == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        _service.DeleteMatricula(id);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void Validaciones(Matricula matricula)
        {
            _validaciones.RequiredId("AlumnoId", matricula.AlumnoId);
            _validaciones.RequiredId("ApoderadoId", matricula.ApoderadoId);
            _validaciones.RequiredId("SeccionId", matricula.SeccionId);
        }

        public JsonResult GetAlumnosBySeccion(int seccionId, string añoEscolar)
        {

            var matriculados = _service.GetMatriculas("").Where(m => m.SeccionId.Equals(seccionId)
                                                                     && m.AnioAcademico.Anio.Equals(añoEscolar));
            if (matriculados.Any())
            {
                var alumnos = _AlumnoService.GetAlumnos("").Where(a => a.Id.Equals(matriculados.First().AlumnoId));
                return Json(alumnos);
            }
            
            return Json("ok");
        }

        public JsonResult GetNivel(int nivelId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var grado = _gradoService.GetGrados().Where(g => g.NivelId == nivelId);
            return Json(grado);
        }

        public JsonResult GetAlumnoMatriculado(int nivelId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var grado = _gradoService.GetGrados().Where(g => g.NivelId == nivelId);
            return Json(grado);
        }

        public JsonResult GetSeccion(int gradoId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            //var seccion = _seccionService.GetSecciones("");
            var seccion = _seccionService.GetSecciones("").Where(s => s.GradoId == gradoId);
            return Json(seccion);
        }

        public JsonResult GetMaxLengthMatriculas(int seccionId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var fecha = DateTime.Now.Year.ToString();
            var matriculados = _service.GetMatriculas("").Count(m => m.SeccionId.Equals(seccionId) && m.AnioAcademico.Anio.Equals(fecha));
            var secciones = _seccionService.GetSecciones("").First(s => s.Id.Equals(seccionId)).Capasida;
            var rpt = matriculados <= secciones ? matriculados.ToString() : "mayor";
            var vacantes = secciones - matriculados;
            return Json(new { rpt,vacantes});
        }

        public JsonResult GetDocumentos(int documentoId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var documento = _AlumnoService.GetAlumnos("").Where(d => d.Documento.Id == documentoId);
            return Json(documento);
        }

        public JsonResult GetAlumnos(int numeroDocumentoId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var alumno = _AlumnoService.GetAlumnos("").Where(a => a.Id == numeroDocumentoId);
            return Json(alumno);
        }

        public JsonResult GetDocumentosApoderados(int documentoIdA)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var documento = _apoderado.GetApoderados("").Where(d => d.Documento.Id == documentoIdA);
            return Json(documento);
        }

        public JsonResult GetsApoderados(int numeroDocumentoIdA)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var apoderado = _apoderado.GetApoderados("").Where(ap => ap.Id == numeroDocumentoIdA);
            return Json(apoderado);
        }

        [HttpPost]
        public JsonResult GetBusca(int NumDoc, string TipoDoc, string estado)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            object data;
            if (estado == "Alumno")
            {
                var alumnoAny = _AlumnoService.GetAlumnos("").
                    Any(a => a.NumeroDocumento.Equals(NumDoc) &&
                               a.Documento.NomDocumento.Equals(TipoDoc));
                if (!alumnoAny)
                {
                    data = new
                    {
                        msj = "noExiste",
                        dni = NumDoc.ToString()
                    };
                    return Json(new {data});
                }

                var alumno = _AlumnoService.GetAlumnos("").
                    First(a => a.NumeroDocumento.Equals(NumDoc) &&
                             a.Documento.NomDocumento.Equals(TipoDoc));
                if (alumno.Estado)
                {
                    data = new
                    {
                        msj = "false"
                    };
                    return Json(new { alumno, data });
                }
                var fecha = DateTime.Now.AddYears(-1).Year;
                var matriculado =_service.GetMatriculas("")
                    .Any(m => m.AlumnoId.Equals(alumno.Id) && m.AnioAcademico.Anio.Equals(fecha.ToString()));


                if (matriculado)
                {
                    var matri= from m in _service.GetMatriculas("").AsQueryable().Include("Alumno")
                            .Include("AnioAcademico")
                        where m.AlumnoId.Equals(alumno.Id) && m.AnioAcademico.Anio.Equals(fecha.ToString())
                        select m;

                    var alm = matri.First();

                    var promedio = from p in _promedio.GetPromedios().AsQueryable().Include("Nota").Include("Nota.Alumno")
                            .Include("Nota.Seccion").Include("Nota.Seccion").Include("Nota.Seccion.Grado").Include("Nota.Seccion.Grado.Nivel")
                        where p.Nota.AlumnoId.Equals(alm.AlumnoId) && p.Nota.Seccion.Id.Equals(alm.SeccionId)
                        select p;

                    var notaFinal = promedio.ToList()[2];

                    var anio = DateTime.Now.Year;
                    var grado = new { notaFinal.Nota.Seccion.Grado.NombreGrado, notaFinal.Nota.Seccion.Grado.Id };
                    var nivel = new { notaFinal.Nota.Seccion.Grado.Nivel.NombreNivel, notaFinal.Nota.Seccion.Grado.Nivel.Id };
                    var seccion = new { notaFinal.Nota.Seccion.NombreSeccion, notaFinal.Nota.Seccion.Id };
                    var cantidad = _service.GetMatriculas("")
                        .Count(m => m.SeccionId.Equals(notaFinal.Nota.Seccion.Id) && m.AnioAcademico.Anio.Equals(anio.ToString()));

                    if (notaFinal.PromedioFinal > 11)
                    {
                        data = new
                        {
                            msj = "Promovido",
                            nota = notaFinal.PromedioFinal,
                            s = seccion,
                            g = grado,
                            n = nivel,
                            maxLength = "0",
                            vacantes = "0"
                        };
                    }
                    else
                    {
                        data = new
                        {
                            msj = "Repite",
                            nota = notaFinal.PromedioFinal,
                            s = seccion,
                            g = grado,
                            n = nivel,
                            maxLength = cantidad,
                            vacantes = notaFinal.Nota.Seccion.Capasida - cantidad
                        };
                    }

                    //var proovido = 
                    return Json(new { alumno, data });
                }
                else
                {
                    var anio = DateTime.Now.Year;
                    var matriculadoExixtente = _service.GetMatriculas("")
                        .Any(m => m.AlumnoId.Equals(alumno.Id) && m.AnioAcademico.Anio.Equals(anio.ToString()));
                    if (matriculadoExixtente)
                    {
                       data = new
                        {
                            msj = "Existe",
                            dni = NumDoc.ToString()
                        };
                        return Json(new { data});
                    }
                    
                }

            }
            else
            {
                var apoderado = _apoderado.GetApoderados("").
                    Where(a => a.NroDocumento.Equals(NumDoc) &&
                               a.Documento.NomDocumento.Equals(TipoDoc));
                return Json(apoderado);
            }
            return null;
        }

        /*-------------------------------Agregar Alumno-------------------------------------------------------*/

        public void Combo()
        {
            ViewBag.documento = _DocumentoService.GetDocumentos().OrderBy(d => d.NomDocumento);

            var DepartamentoId = Departamentos();
            ViewBag.departamento = new SelectList(DepartamentoId, "Text", "Value");

            var ProvinciaId = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione", Value = "Seleccione" }
            };
            ViewBag.provincia = new SelectList(ProvinciaId, "Text", "Value");

            var DistritoId = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione", Value = "Seleccione" }
            };
            ViewBag.distrito = new SelectList(DistritoId, "Text", "Value");
        }
        public List<SelectListItem> Departamentos()
        {
            List<SelectListItem> DepartamentoId = new List<SelectListItem>();
            var dato = _ubigeo.GetUbigeos().Where(u => u.IdUbigeo.Remove(0, 2).Equals("0000"));
            foreach (var item in dato)
            {
                DepartamentoId.Add(
                    new SelectListItem()
                    {
                        Text = item.IdUbigeo,
                        Value = item.Departamento
                    });
            }
            return DepartamentoId;
        }

        public ActionResult CreateAlumno()
        {

            Combo();
            return PartialView("CreateAlumno");
        }

        [HttpPost]
        public ActionResult CreateAlumno(Alumno alumno)
        {
            try
            {
                Combo();
                ValidacionesAlumno(alumno);
                if (ModelState.IsValid) //validacion del si el modelo existe
                {
                    if (alumno.Id == 0)

                    {
                        if (!_AlumnoService.GetAlumnos("").Any(p => p.NumeroDocumento.Equals(alumno.NumeroDocumento))) //si no hay alumnos regristrados
                        {
                            alumno.Estado = true; //es nuevo
                            _AlumnoService.AddAlumno(alumno); //agega el nuevo dato
                            return Json(new { data = "ok" });
                        }
                    }
                }
                return PartialView(alumno);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public void ValidacionesAlumno(Alumno alumno)
        {
            _validaciones.Required("Nombre", alumno.Nombre);
            _validaciones.MaxLength("Nombre", alumno.Nombre, 50);
            _validaciones.Required("ApMaterno", alumno.ApMaterno);
            _validaciones.MaxLength("ApMaterno", alumno.ApMaterno, 50);
            _validaciones.Required("ApPaterno", alumno.ApPaterno);
            _validaciones.MaxLength("ApPaterno", alumno.ApPaterno, 50);
            _validaciones.Required("Genero", alumno.Genero);
            _validaciones.Required("Direccion", alumno.Direccion);
            _validaciones.MaxLength("Direccion", alumno.Direccion, 50);
            _validaciones.Required("Correo", alumno.Correo);
            _validaciones.MaxLength("Correo", alumno.Correo, 50);
            _validaciones.Email("Correo", alumno.Correo);
            _validaciones.RequiredNro("Telefono", alumno.Telefono);
            _validaciones.RequiredNro("NumeroDocumento", alumno.NumeroDocumento);
            _validaciones.MaxLengthDocumento("NumeroDocumento", alumno.NumeroDocumento.ToString(), 8);
            _validaciones.RequiredFechas("FechaNacimiento", alumno.FechaNacimiento);
            _validaciones.FechaMayor("FechaNacimiento", alumno.FechaNacimiento);
            _validaciones.RequiredId("DocumentoId", alumno.DocumentoId);
            _validaciones.Required("IdUbigeo", alumno.IdUbigeo);

        }

        /*--------------------------------------------------------------------------------------*/
        /*-------------------------------Agregar Apoderado-------------------------------------------------------*/

        public ActionResult CreateApoderdo()
        {
            Combo();
            return PartialView("_CreateApoderado");
        }

        [HttpPost]
        public ActionResult CreateApoderdo(Apoderado apoderado)
        {
            try
            {
                Combo();
                ValidacionesApoderado(apoderado);
                if (ModelState.IsValid)
                {
                    if (apoderado.Id == 0)

                    {
                        if (!_apoderado.GetApoderados("").Any(p => p.NroDocumento.Equals(apoderado.NroDocumento))) //si no hay alumnos regristrados
                        {
                            _apoderado.AddApoderado(apoderado); //agega el nuevo dato
                            return Json(new { data = "ok" });
                        }
                    }
                }

                return PartialView("_CreateApoderado", apoderado);
            }
            catch
            {
                return View();
            }
        }

        public void ValidacionesApoderado(Apoderado apoderado)
        {
            _validaciones.Required("Nombres", apoderado.Nombres);
            _validaciones.MaxLength("Nombres", apoderado.Nombres, 50);
            _validaciones.Required("ApMaterno", apoderado.ApMaterno);
            _validaciones.MaxLength("ApMaterno", apoderado.ApMaterno, 50);
            _validaciones.Required("ApPaterno", apoderado.ApPaterno);
            _validaciones.MaxLength("ApPaterno", apoderado.ApPaterno, 50);
            _validaciones.Required("Genero", apoderado.Genero);
            _validaciones.Required("Direccion", apoderado.Direccion);
            _validaciones.MaxLength("Direccion", apoderado.Direccion, 50);
            _validaciones.Required("Correo", apoderado.Correo);
            _validaciones.MaxLength("Correo", apoderado.Correo, 50);
            _validaciones.Email("Correo", apoderado.Correo);
            _validaciones.RequiredNro("Telefono", apoderado.Telefono);
            _validaciones.RequiredNro("NroDocumento", apoderado.NroDocumento);
            _validaciones.RequiredNro("NroDocumento", apoderado.NroDocumento);
            _validaciones.MaxLengthDocumento("NroDocumento", apoderado.NroDocumento.ToString(),8);
            _validaciones.RequiredFechas("FechaNacimiento", apoderado.FechaNacimiento);
            _validaciones.FechaMayorApoderado("FechaNacimiento", apoderado.FechaNacimiento);
            _validaciones.RequiredId("DocumentoId", apoderado.DocumentoId);
            _validaciones.Required("IdUbigeo", apoderado.IdUbigeo);

        }

        /*--------------------------------------------------------------------------------------*/
    }
}