using JPCSystem.Domain;
using JPCSystem.Service;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class AnioEscolarController : Controller
    {
        private IAnioEscolarService _service;
        private readonly ValidacionesUsuario _validaciones;

        public AnioEscolarController(IAnioEscolarService service)
        {
            _service = service;
            _validaciones = new ValidacionesUsuario(ModelState);
        }

        // GET: AñoEscolar
        public ActionResult Index()
        {
            var lista = _service.GetAniosAcademicos("");
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(string criterio)
        {
            var lista = _service.GetAniosAcademicos(criterio);
            return PartialView("_listaAnios", lista);
        }

        // GET: AñoEscolar/Details/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AñoEscolar/Create
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Create()
        {
            var fecha = DateTime.Now.AddYears(1).Year.ToString();
            var anios = _service.GetAniosAcademicos("").Where(a => a.Anio.Equals(fecha));
            if (!anios.Any())
            {
                var anio = new AnioAcademico
                {
                    Anio = DateTime.Now.AddYears(1).Year.ToString(),
                };

                return PartialView("Create", anio);
            }

            var data = new
            {
                msg = "ok",
                fecha = DateTime.Now.AddYears(1).Year.ToString()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // POST: AñoEscolar/Create
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

    public ActionResult Create(AnioAcademico anioAcademico)
        {
            try
            {
                var fecha = DateTime.Now.AddYears(1).Year.ToString();
                if (int.Parse(fecha) > int.Parse(anioAcademico.Anio))
                {
                    fecha = anioAcademico.Anio;
                }
                Validaciones(anioAcademico,fecha);
                if (ModelState.IsValid)
                {
                    if (anioAcademico.Id==0)
                    {
                        if (!ExiteAnioEscolar(anioAcademico))
                        {
                            _service.AddAnioAcademico(anioAcademico);
                            var anios = _service.GetAniosAcademicos("");
                            return Json(new
                            {
                                info = PartialView(this, "_listaAnios",anios ),
                                rpt = "true"
                            });
                        }
                        return Json(new { info = "", rpt = "Existe" });

                    }
                    if (anioAcademico.Id>0)
                    {
                        _service.UpdateAnioAcademico(anioAcademico);
                        return Json(new
                        {
                            info = PartialView(this, "_listaAnios", _service.GetAniosAcademicos("")),
                            rpt = "true"
                        });
                    }
                    
                }
                anioAcademico.Anio = fecha;
                return Json(new { info = PartialView(this, "Create", anioAcademico), rpt = "false" });
            }
            catch (Exception e)
            {
                return Json(new { info = e.Message , rpt = "Error" });
            }
            //return View();
        }

        public bool ExiteAnioEscolar(AnioAcademico anio)
        {
            return _service.GetAniosAcademicos("").Any(a => a.Anio.Equals(anio.Anio));
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
        // GET: AñoEscolar/Edit/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {

            var anio = _service.GetAnioAcademico(id);
            return PartialView("_Edit",anio);
        }

        // POST: AñoEscolar/Edit/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Edit(AnioAcademico anioAcademico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.AddAnioAcademico(anioAcademico);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AñoEscolar/Delete/5
        public ActionResult Delete(int id)
        {
            var lista = _service.GetAnioAcademico(id);
            return View(lista);
        }

        // POST: AñoEscolar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AnioAcademico anioAcademico)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    anioAcademico = _service.GetAnioAcademico(id);

                    if (anioAcademico == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        _service.DeleteAnioAcademico(id);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void Validaciones(AnioAcademico anio,string fecha)
        {
            _validaciones.RequiredFechas("FechaInicioMatricula", anio.FechaInicioMatricula);
            _validaciones.RequiredFechas("FechaFinMatricula", anio.FechaFinMatricula);
            _validaciones.RequiredFechas("FechaMatriculaExtemporanea", anio.FechaMatriculaExtemporanea);
            _validaciones.RequiredFechas("FechaFin", anio.FechaFin);
            _validaciones.RequiredFechas("FechaInicio", anio.FechaInicio);
            _validaciones.Required("Anio", anio.Anio);

            _validaciones.VerificaRangos("FechaInicioMatricula", anio.FechaInicioMatricula, anio.FechaFinMatricula);
            _validaciones.VerificaRangos("FechaFinMatricula", anio.FechaFinMatricula, anio.FechaMatriculaExtemporanea);
            _validaciones.VerificaRangosF_E("FechaMatriculaExtemporanea", anio.FechaMatriculaExtemporanea, anio.FechaFinMatricula);
            _validaciones.VerificaRangos("FechaInicio", anio.FechaInicio, anio.FechaFin);
            //_validaciones.VerificaRangos("FechaInicioTercTrim", aperturar.FechaInicioTercTrim, aperturar.FechaFinTercTrim);

            _validaciones.VerificaFechaConAnioAcademico("FechaInicio", anio.FechaInicio, fecha);
            _validaciones.VerificaFechaConAnioAcademico("FechaFin", anio.FechaFin, fecha);
            _validaciones.VerificaFechaConAnioAcademico("FechaInicioMatricula", anio.FechaInicioMatricula, fecha);
            _validaciones.VerificaFechaConAnioAcademico("FechaFinMatricula", anio.FechaFinMatricula, fecha);
            _validaciones.VerificaFechaConAnioAcademico("FechaMatriculaExtemporanea", anio.FechaMatriculaExtemporanea, fecha);
        }
    }
}