using JPCSystem.Service;
using JPCSystem.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class NivelController : Controller
    {
        // GET: Nivel
        private ApplicationDbContext _context = new ApplicationDbContext();

        private INivelService _nivel;
        private readonly ValidacionesDeGestionarAulas _validators;

        public NivelController(INivelService nivel)
        {
            _nivel = nivel;
            _validators = new ValidacionesDeGestionarAulas(ModelState,nivel);
        }

        public ActionResult Index()
        {
            return View("Index", _nivel.GetNiveles());
        }

        // GET: Nivel/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Nivel/Create
        [HttpPost]
        public ActionResult Create(Nivel nivel)
        {
            Validaciones(nivel);

            try
            {
                if (ModelState.IsValid)
                {
                    if (nivel.Id==0)
                    {
                        _nivel.AddNivel(nivel);
                        
                    }
                    if (nivel.Id>0)
                    {
                        var n = _nivel.GetNivel(nivel.Id);
                        n.NombreNivel = nivel.NombreNivel;
                        _nivel.UpdateNivel(n);
                    }
                    return Json(new {info = PartialView(this,"_Index",_nivel.GetNiveles()), msj = "ok"});
                }
                return PartialView("_Create",nivel);

            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        public void Validaciones(Nivel nivel)
        {
            _validators.RequiredNombre("NombreNivel", nivel.NombreNivel);
            _validators.RetipeNombreDeNivel("NombreNivel",nivel.NombreNivel);
          
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
        // GET: Nivel/Edit/5
        public ActionResult Edit(int id)
        {
            var nivel = _nivel.GetNivel(id);
            return PartialView("_Edit", nivel);
        }

        // POST: Nivel/Edit/5
        [HttpPost]
        public ActionResult Edit(Nivel nivel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _nivel.UpdateNivel(nivel);
                }
                return View("Index", _nivel.GetNiveles());
            }
            catch
            {
                return View("_Edit");
            }
        }

        // GET: Nivel/Delete/5
       
        public ActionResult Delete(int id)
        {
            try
            {
                if (_nivel.GetNivel(id) != null)
                {
                    _nivel.DeleteNivel(id);
                }
                return PartialView("_Index", _nivel.GetNiveles());
            }
            catch (Exception)
            {
                return View("Index");
            }
        }
    }
}