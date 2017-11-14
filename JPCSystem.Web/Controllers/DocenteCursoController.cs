using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using JPCSystem.Web.validaciones;
using Microsoft.Ajax.Utilities;

namespace JPCSystem.Web.Controllers
{
    public class DocenteCursoController : Controller
    {
        private IDocenteCursoService _service;
        private INivelService _nivel;
        private IGradoService _grado;
        private ISeccionService _seccion;
        private IDocenteService _docente;
        private ICursoService _curso;
        private readonly ValidacionesUsuario _validators;
        ApplicationDbContext _context = new ApplicationDbContext();

        public DocenteCursoController(IDocenteCursoService service, INivelService nivel, IGradoService grado, IDocenteService docente, ICursoService curso
            ,ISeccionService seccion )
        {
            _service = service;
            _nivel = nivel;
            _grado = grado;
            _docente = docente;
            _curso = curso;
            _seccion = seccion;

            _validators = new ValidacionesUsuario(ModelState);
        }
        

        // GET: DocenteCurso
        public ActionResult Index()
        {
            ViewBag.Docente = new SelectList(_docente.GetDocentes(""),"Id", "NombreCompleto");
            ViewBag.cursos = new SelectList(_curso.GetCursos(""), "Id", "NombreCurso");
            ViewBag.nivel = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            var lista = _service.GetDocenteCursos("");
            return View(lista);
        }

        public ActionResult CreateCurso()
        {
            ViewBag.nivel = _nivel.GetNiveles();
            ViewBag.grado = _grado.GetGrados();
            return PartialView("_Crear");
        }

        [HttpPost]
        public ActionResult CreateCurso(Curso curso)
        {
            try
            {
                ViewBag.nivel = _nivel.GetNiveles();
                ViewBag.grado = _grado.GetGrados();
                if (ModelState.IsValid)
                {
                    _curso.AddCurso(curso);
                    return Json(new
                    {
                        info = PartialView(this, "_HorarioLista", _service.GetDocenteCursos("")),
                        rpt = "true"
                    });
                }
                return Json(new { info = PartialView(this, "_Crear", curso), rpt = "false" });

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string criterio,int? cursoId,int? nivelId,int? docenteId)
        {
            var idC = (cursoId ?? 0);
            var idN = (nivelId ?? 0);
            var idD = (docenteId ?? 0);

            var  lista = _service.GetDocenteCursos("");
            if (!String.IsNullOrEmpty(criterio) && idC == 0 && idN == 0 && idD == 0)
            {
                lista = _service.GetDocenteCursos(criterio);
            }
            if (!String.IsNullOrEmpty(criterio) && idC > 0 && nivelId == 0 && docenteId == 0)
            {
                lista = _service.GetDocenteCursos(criterio).Where(p=>p.CursoId.Equals(idC));
            }
            if (!String.IsNullOrEmpty(criterio) && idC > 0 && idN > 0 && idD == 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso")
                    where l.Curso.NivelId.Equals(idN) 
                    && l.CursoId.Equals(idC)
                          && l.Seccion.Grado.NombreGrado.ToUpper().Contains(criterio.ToUpper())
                        select l;
            }

            if (String.IsNullOrEmpty(criterio) && idC > 0 && idN > 0 && idD == 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso")
                    where l.Curso.NivelId.Equals(idN)
                          && l.CursoId.Equals(idC)
                    select l;
            }

            if (!String.IsNullOrEmpty(criterio) && idC > 0 && idN > 0 && idD > 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso")
                    where l.DocenteId.Equals(idD) && l.Curso.NivelId.Equals(idN)
                    && l.CursoId.Equals(idC)
                    && l.Seccion.Grado.NombreGrado.ToUpper().Contains(criterio.ToUpper())
                        select l;
            }
            if (String.IsNullOrEmpty(criterio) && idC > 0 && idN > 0 && idD > 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso").Include("Seccion").Include("Seccion.Grado")
                    where l.DocenteId.Equals(idD) 
                            && l.Curso.NivelId.Equals(idN)
                            && l.CursoId.Equals(idC) 
                    select l;
            }

            if (String.IsNullOrEmpty(criterio) && idC == 0 && idN > 0 && idD == 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso").Include("Seccion").Include("Seccion.Grado")
                    where l.Curso.NivelId.Equals(idN)
                    select l;
            }
            if (!String.IsNullOrEmpty(criterio) && idC == 0 && idN > 0 && idD == 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso").Include("Seccion").Include("Seccion.Grado")
                    where l.Curso.NivelId.Equals(idN) 
                    && l.Seccion.Grado.NombreGrado.ToUpper().Contains(criterio.ToUpper())
                        select l;
            }
            if (String.IsNullOrEmpty(criterio) && idC == 0 && idN == 0 && idD > 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso").Include("Seccion").Include("Seccion.Grado")
                    where l.DocenteId.Equals(idD)
                        select l;
            }

            if (!String.IsNullOrEmpty(criterio) && idC == 0 && idN == 0 && idD > 0)
            {
                lista = from l in lista.AsQueryable().Include("Curso").Include("Seccion").Include("Seccion.Grado")
                    where l.DocenteId.Equals(idD) &&
                          l.Seccion.Grado.NombreGrado.ToUpper().Contains(criterio.ToUpper())
                        select l;
            }

            return PartialView("_HorarioLista",lista);
        }

        //public ActionResult DetallesCursosByDocente(int id)
        //{
        //    var dc = _service.GetDocenteCurso(id);
        //    var curosByDocente = _service.GetDocenteCursos("").Where(d => d.DocenteId.Equals(dc.DocenteId));
        //    return PartialView("_DetalleCursosByDocente", curosByDocente);
        //}

        // GET: DocenteCurso/Create
        public ActionResult Create()
        {
            CombosSelect();
            return PartialView("Create");
        }

        public void CombosSelect()
        {

            ViewBag.nivel = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            ViewBag.grado =
                new SelectList(new List<SelectListItem>{
                    new SelectListItem {
                        Text = "Seleccione...",
                        Value = "Seleccione"
                    }
                }, "Text", "Value");

            ViewBag.Curso = new SelectList(
                new List<SelectListItem>{
                    new SelectListItem {
                        Text = "Seleccione...",
                        Value = "Seleccione"
                    }
                }, "Text", "Value");

            ViewBag.seccion = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {
                        Text = "Seleccione...",
                        Value = "Seleccione"
                    }
                }
                , "Text", "Value");

            ViewBag.dia = Dias();

            ViewBag.Docente = _docente.GetDocentes("");
        }

        public SelectList Dias()
        {
            var dias = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {
                        Text = "Lunes",
                        Value = "Lunes"
                    },
                    new SelectListItem {
                        Text = "Martes",
                        Value = "Martes"
                    },
                    new SelectListItem {
                        Text = "Miercoles",
                        Value = "Miercoles"
                    },
                    new SelectListItem {
                        Text = "Jueves",
                        Value = "Jueves"
                    },
                    new SelectListItem {
                        Text = "Viernes",
                        Value = "Viernes"
                    },
                }
                , "Text", "Value");
            return dias;
        }
        // POST: DocenteCurso/Create
        [HttpPost]
        public ActionResult Create(DocenteCurso docenteCurso)
        {
            
            try
            {
                CombosSelect();
                Validaciones(docenteCurso);
                if (ModelState.IsValid)
                {
                    if (docenteCurso.Id == 0)
                    {
                        if (!ExisteHorario(docenteCurso))
                        {
                            _service.AddDocenteCurso(docenteCurso);
                            return Json(new
                            {
                                info = PartialView(this, "_HorarioLista", _service.GetDocenteCursos("")),
                                rpt = "true"
                            });
                        }
                        return Json(new { info = "", rpt = "Existe" });

                    }
                    if (docenteCurso.Id > 0)
                    {
                        _service.UpdateDocenteCurso(docenteCurso);
                        return Json(new
                        {
                            info = PartialView(this, "_DocenteLista", _service.GetDocenteCursos("")), rpt = "true"
                        });
                    }
                }
                
                return Json(new { info = PartialView(this, "Create", docenteCurso), rpt = "false" });
            }
            catch
            {
                return View("Index",_service.GetDocenteCursos(""));
            }
        }

        public bool ExisteHorario(DocenteCurso dc)
        {
            var Inicio = dc.HoraInicio;
            var fin = dc.HoraFin;

            var docenteDictaClase = _service.GetDocenteCursos("").Any(d => (d.DocenteId.Equals(dc.DocenteId) || d.DocenteId != dc.DocenteId) &&
                                                                           d.HoraInicio.Equals(Inicio) &&
                                                                           d.HoraFin.Equals(fin) && 
                                                                           d.Dia.Equals(dc.Dia) && 
                                                                           d.SeccionId.Equals(dc.SeccionId));
            return docenteDictaClase;
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

        // GET: DocenteCurso/Edit/5
        public ActionResult Edit(int id)
        {
            var docenteCurso = _service.GetDocenteCurso(id);
            AsiganaDatosACombosEdit(id,docenteCurso);
            return PartialView("_Edit", docenteCurso);
        }

        public void AsiganaDatosACombosEdit(int id,DocenteCurso docenteCurso)
        {
            
            var idNivel = _curso.GetCurso(docenteCurso.CursoId).NivelId;
            var idGrado = _seccion.GetSeccion(docenteCurso.SeccionId).GradoId;
            ViewBag.Docente = _docente.GetDocentes("");
            ViewBag.nivel = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            ViewBag.grado = new SelectList(_grado.GetGrados().Where(n => n.NivelId.Equals(idNivel)), "Id", "NombreGrado");
            ViewBag.seccion = new SelectList(_seccion.GetSecciones("").Where(g => g.GradoId.Equals(idGrado)), "Id", "NombreSeccion");
            ViewBag.curso = new SelectList(_curso.GetCursos("").Where(n => n.NivelId.Equals(idNivel)), "Id", "NombreCurso");
            ViewBag.dia = Dias();
            docenteCurso.NivelId = idNivel;
            docenteCurso.GradoId = idGrado;
        }

        public JsonResult GetNivel(int nivelId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
           var grado = _grado.GetGrados().Where(g => g.NivelId == nivelId);
            var curso = _curso.GetCursos("").Where(c => c.NivelId.Equals(nivelId));
            return Json(new { grado, curso});
        }

        public JsonResult GetSeccion(int gradoId)
        {
            var seccion = _seccion.GetSecciones("").Where(s => s.GradoId.Equals(gradoId));
            return Json(seccion);
        }

        public void Validaciones(DocenteCurso dc)
        {
            _validators.Required("HoraInicio", dc.HoraInicio);
            _validators.Required("HoraFin", dc.HoraFin);
            _validators.Required("Dia", dc.Dia);
            _validators.RequiredId("NivelId", dc.NivelId);
            _validators.RequiredId("GradoId", dc.GradoId);
            _validators.RequiredId("SeccionId", dc.SeccionId);
            _validators.RequiredId("CursoId", dc.CursoId);
            _validators.RequiredId("DocenteId", dc.DocenteId);
            //_validators.Required("Direccion", dc.Direccion);
            //_validators.MaxLength("Direccion", dc.Direccion, 50);
            //_validators.RequiredNro("Telefono", dc.Telefono);
            //_validators.RequiredNro("NroDocumento", dc.NroDocumento);
            //_validators.RequiredFechas("FechaNacimiento", dc.FechaNacimiento);
            //_validators.FechaMayor("FechaNacimiento", dc.FechaNacimiento);
            //_validators.RequiredId("DocumentoId", dc.DocumentoId);
            //_validators.Required("IdUbigeo", dc.IdUbigeo);

        }


    }
}
