using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class CursoController : Controller
    {
        private ICursoService _service;
        private INivelService _nivel;
        private IGradoService _grado;
        private ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ValidacionesCurso _validators;

        public CursoController(ICursoService service, INivelService nivel, IGradoService grado)
        {
            _service = service;
            _nivel = nivel;
            _grado = grado;
            _validators = new ValidacionesCurso(ModelState);
        }



        // GET: Curso
        public ActionResult Index()
        {
            var lista = _service.GetCursos("");
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(String criterio)
        {
            var lista = _service.GetCursos(criterio);
            return View(lista);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            Ubicacion();
            return PartialView("Create");
        }

        // POST: Curso/Create
        [HttpPost]
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]
        public ActionResult Create(Curso curso)
        {
            Validaciones(curso);
            //var idC = curso.Id ?? 0; 
            try
            {
                var idC = TempData["id"] ?? 0;
                Ubicacion();
                if (ModelState.IsValid)
                {
                    if (int.Parse(idC.ToString()) == 0)
                    {
                        if (!_service.GetCursos("").Any(a => a.GradoId.Equals(curso.GradoId)
                                                             &&
                                                             a.NombreCurso.ToUpper().Equals(curso.NombreCurso.ToUpper())))
                        {
                            _service.AddCurso(curso);
                            return Json(new
                            {
                                info = PartialView(this, "_ListaCursos", _service.GetCursos("")),
                                rpt = "true"
                            });
                        }
                        return Json(new
                        {
                            info = "",
                            rpt = "Existe"
                        });
                    }
                    if (int.Parse(idC.ToString()) > 0)
                    {
                        var c = _service.GetCurso(int.Parse(idC.ToString()));
                        c.GradoId = curso.GradoId;
                        c.Id = int.Parse(idC.ToString());
                        c.NombreCurso = curso.NombreCurso;
                        _service.UpdateCurso(c);
                        return Json(new
                        {
                            info = PartialView(this, "_ListaCursos", _service.GetCursos("")),
                            rpt = "edit"
                        });

                    }

                }
                return Json(new {info = PartialView(this, "Create", curso), rpt = "false"});

            }
            catch
            {
                return View();
            }
        }

        public static string PartialView(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                    controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }

        public void Validaciones(Curso curso)
        {
            _validators.Required("NombreCurso", curso.NombreCurso);
            _validators.RequiredId("GradoId", curso.GradoId);
            _validators.RequiredId("NivelId", curso.NivelId);
            // _validators.RetipeNombreDeCursoAndIdNivel("NombreCurso", curso.NombreCurso,curso.NivelId);

        }

        // GET: Curso/Edit/5
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]

    public ActionResult Edit(int id)
        {
            TempData.Remove("id");
            TempData["id"] = id;
            var curso = _service.GetCurso(id);
            var grado = _grado.GetGrado(curso.GradoId);

            var grados = _grado.GetGrados().Where(g=> g.NivelId.Equals(grado.NivelId)).OrderBy(x => x.NombreGrado);
            ViewBag.Grados = new SelectList(grados, "Id", "NombreGrado");

            var niveles = _nivel.GetNiveles().OrderBy(x => x.NombreNivel);
            ViewBag.Niveles = new SelectList(niveles, "Id", "NombreNivel");

            curso.NivelId = _nivel.GetNivel(grado.NivelId).Id;
            return PartialView("Create",curso);
        }
        public JsonResult GetProvinciasByDepartamento(int? id)
        {
            var lista = id != null ? _grado.GetGrados().Where(o=>o.NivelId == id).ToList() : new List<Grado>();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public void Ubicacion()
        {
            //Nivels
            var niveles = _nivel.GetNiveles().OrderBy(x => x.NombreNivel);
            ViewBag.Niveles = new SelectList(niveles, "Id", "NombreNivel");

            //Grados
            var grados = _grado.GetGrados().OrderBy(x => x.NombreGrado);
            ViewBag.Grados = new SelectList(new List<SelectListItem>{
                new SelectListItem() {
                    Text = "Seleccione",
                    Value = "Seleccione"
                }
            }, "Value", "Text");
        }
        // POST: Curso/Edit/5
        [HttpPost]
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]
        public ActionResult Edit(Curso curso)
        {
            //var grado = _grado.GetGrado(curso.GradoId);
            //var nivel = _nivel.GetNivel(curso.NivelId);
            //ViewBag.niveles = _nivel.GetNiveles().Where(g => g.Id.Equals(nivel.Id));
            //ViewBag.grados = _grado.GetGrados().Where(g => g.NivelId.Equals(grado.NivelId));
            Ubicacion();
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateCurso(curso);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int id)
        {
            var lista = _service.GetCurso(id);
            return View(lista);
        }

        // POST: Curso/Delete/5
        [HttpPost]
        //[Authorize(Roles = "admin,docente")]
        [AllowAnonymous]
        public ActionResult Delete(int id,Curso curso)
        {
            try
            {
                    curso = _service.GetCurso(id);
                    if (curso!=null)
                        _service.DeleteCurso(id);

                    else return HttpNotFound();

                return Json(new
                {
                    info = PartialView(this, "_ListaCursos", _service.GetCursos("")),
                    rpt = "ok"
                });
            }
            catch
            {
                return View();
            }
        }
        public JsonResult getNiveles(int nivelId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var grado = _grado.GetGrados().Where(g => g.NivelId == nivelId);
            return Json(grado,JsonRequestBehavior.AllowGet);
        }
    }
}
