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
using PagedList;

namespace JPCSystem.Web.Controllers
{
    public class SeccionesController : Controller
    {
        //CORREGIR LA PAGINACION DEL CREATE
        private ApplicationDbContext _context = new ApplicationDbContext();
        private ISeccionService _service;
        private INivelService _nivel;
        private IGradoService _gradoService;
        private IMatriculaService _matricula;
        private readonly ValidacionesDeGestionarAulas _validators;

        public SeccionesController(ISeccionService service, INivelService nivel, 
            IGradoService gradoService,IMatriculaService matricula)
        {
            _service = service;
            _nivel = nivel;
            _gradoService = gradoService;
            _matricula = matricula;
           _validators = new ValidacionesDeGestionarAulas(ModelState, service);
        }

        // GET: Secciones
        [HttpGet]
        public ActionResult Index(int? page)
        {
            ViewBag.page = page ?? 1;
            var pageIndex = page ?? 1;
            var lista = _service.GetSeccionesQueryable().ToPagedList(pageIndex, 5);

            foreach (var a in lista)
            {
                a.Alumnos = _matricula.GetAlumnos(a.Id).Any() ? _matricula.GetAlumnos(a.Id).Count() : 0;
            }
            //return View(lista);
            //if (Request.IsAjaxRequest())
            //{
            //    return (ActionResult) PartialView("_Index");
            //}
            //else
            //{
            //    return View(lista);
            //}
         
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("_Index", lista) : View(lista);
        }

        public void Validaciones(Seccion seccion)
        {
            _validators.RequiredNombre("NombreSeccion", seccion.NombreSeccion);
            _validators.RequiredId("NivelId", seccion.NivelId);
            _validators.RequiredId("GradoId", seccion.GradoId);
           // _validators.RetipeNombreDeSeccion("NombreSeccion", seccion.NombreSeccion,seccion.Grado.Nivel.NombreNivel,seccion.Grado.NombreGrado);

        }

        [HttpPost]
        public PartialViewResult Index()
        {
            var lista = _service.GetSecciones("");
            return PartialView("_Index", lista);
           
        }

        // GET: Secciones/Create
        public ActionResult Create(int? page)
        {
            ViewBag.page = page ?? 0;
            var grados = new List<SelectListItem>() {
                new SelectListItem() { Text = "Seleccione", Value = "0" }
            };
            ViewBag.grados = new SelectList(grados, "Value", "Text");
            ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            return PartialView("_Create",new Seccion{Id = 0});
        }

        // POST: Secciones/Create
        [HttpPost]
        public ActionResult Create(Seccion seccion)
        {
           Validaciones(seccion);
            try
            {
                ViewBag.grados = new SelectList(_gradoService.GetGrados(), "Id", "NombreGrado");
                ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
                if (ModelState.IsValid)
                {
                    if (seccion.Id==0)
                    {
                        if (!_service.GetSecciones("").Any(
                            s => s.NombreSeccion.ToUpper().Equals(seccion.NombreSeccion.ToUpper())
                                 && s.GradoId.Equals(seccion.GradoId)))
                        {
                            seccion.Capasida = 20;
                            _service.AddSeccion(seccion);
                            return Request.IsAjaxRequest() ? (ActionResult)PartialView("_Index", _service.GetSeccionesQueryable().ToPagedList(1, 5)) : View("_Index", _service.GetSeccionesQueryable().ToPagedList(1, 5));

                        }
                        return Json(new { info = "", rpt = "existe" });
                    }
                   if (seccion.Id>0)
                   {
                       var secc = _service.GetSeccion(seccion.Id);
                       secc.Id = seccion.Id;
                       secc.GradoId = seccion.GradoId;
                       secc.NivelId = seccion.NivelId;
                       secc.Alumnos = seccion.Alumnos;
                       secc.NombreSeccion = seccion.NombreSeccion;
                       secc.Capasida = 20;
                       _service.UpdateSeccion(secc);
                    }

                    var lista = _service.GetSeccionesQueryable().ToPagedList(1, 5);
                    //return Request.IsAjaxRequest() ? (ActionResult)PartialView("_Index", lista) : View("_Index", lista);
                    return Json(new { info = PartialView(this, "_Index", lista), rpt = "ok" });
                }

                return Json(new { info = PartialView(this, "_Create", seccion),rpt = "error"});
            } 
            catch
            {
                var lista = _service.GetSeccionesQueryable().ToPagedList(1, 5);
                return Json(new { info = PartialView(this, "_Index", lista), rpt = "ok" });
            }
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

        // GET: Secciones/Edit/5
        public ActionResult Edit(int id)
        {
            var seccion = _service.GetSeccion(id);
            var nivel = _nivel.GetNivel(_gradoService.GetGrado(seccion.GradoId).NivelId);
            ViewBag.grados = new SelectList(_gradoService.GetGrados(), "Id", "NombreGrado");
            ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            seccion.NivelId = nivel.Id;

            return PartialView("_Create", seccion);
        }

        // GET: Secciones/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (_service.GetSeccion(id) != null)
                {
                    _service.DeleteSeccion(id);
                }

                //return PartialView("_Index", _service.GetSecciones(""));
                var lista = _service.GetSeccionesQueryable().ToPagedList(1, 3);
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("_Index", lista) : View("Index",lista);
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetNivel(int id)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var grado = _gradoService.GetGrados().Where(g => g.NivelId == id);
            return Json(grado);
        }
    }
}