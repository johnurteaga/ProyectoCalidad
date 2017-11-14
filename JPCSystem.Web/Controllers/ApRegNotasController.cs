using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class ApRegNotasController : Controller
    {
        private IRegNotasService _service;
        private IAnioEscolarService _anio;
        private ITrimestreService _trimestre;
        private readonly ValidacionesUsuario _validaciones;

        public ApRegNotasController(IRegNotasService service, IAnioEscolarService anio, ITrimestreService trimestre)
        {
            _service = service;
            _anio = anio;
            _trimestre = trimestre;
            _validaciones = new ValidacionesUsuario(ModelState);
        }

        // GET: ApRegNotas
        public ActionResult Index()
        {
            var fecha = DateTime.Now.Year.ToString();
            var lista = _service.GetRegistrosNotas("").Where(s => s.AñoAcademico.Anio.Equals(fecha))
                .OrderByDescending(a => a.Id);
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(String criterio)
        {
            var fecha = DateTime.Now.Year.ToString();

            var lista = string.IsNullOrEmpty(criterio)
                ? _service.GetRegistrosNotas(criterio)
                : _service.GetRegistrosNotas(criterio).Where(s => s.AñoAcademico.Anio.Equals(fecha));

            return PartialView("_ListaRegistro", lista);
        }

        // GET: ApRegNotas/Details/5
        public ActionResult Details(int id)
        {
            var lista = _service.GetRegistroNotas(id);
            return View(lista);
        }

        // GET: ApRegNotas/Create
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

        public ActionResult Create()
        {
            ViewBag.trimestre = new SelectList(_trimestre.GeTrimestres(), "Id", "Descripcion");
            var actual = DateTime.Now.Year.ToString();
            var anio = _anio.GetAniosAcademicos("").Where(a => a.Anio.Equals(actual));
            var consulta = _service.GetRegistrosNotas("").Count(a => a.AñoAcademicoId.Equals(anio.Max().Id));
            var anioAcademicos = anio as AnioAcademico[] ?? anio.ToArray();
            if (consulta < 3)
            {
                if (anioAcademicos.Any())
                {
                    if (int.Parse(anioAcademicos.Max().Anio) == int.Parse(DateTime.Now.Year.ToString()))
                    {
                        return PartialView("Create", new AperturarRegistroNotas
                        {
                            nombreAño = anioAcademicos.Max().Anio,
                            AñoAcademicoId = anioAcademicos.Max().Id
                        });
                    }
                }
            }
            var data = new
            {
                fecha = actual,
                rpt = "ok"
            };
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        // POST: ApRegNotas/Create
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

        public ActionResult Create(AperturarRegistroNotas aperturarRegistro)
        {
            try
            {
                ViewBag.trimestre = new SelectList(_trimestre.GeTrimestres(), "Id", "Descripcion");
                var anio = _anio.GetAniosAcademicos("").Where(a => a.Id.Equals(aperturarRegistro.AñoAcademicoId)).Max();
                Validaciones(aperturarRegistro, anio.Anio);
                if (ModelState.IsValid && ComparaFechas(aperturarRegistro))
                {
                    if (aperturarRegistro.Id == 0)
                    {
                        if (!ExiteRegistro(aperturarRegistro))
                        {
                            _service.AddRegistroNotas(aperturarRegistro);

                            return Json(new
                            {
                                info =
                                    PartialView(this, "_ListaRegistro",
                                        _service.GetRegistrosNotas("").OrderByDescending(a => a.Id)),
                                rpt = "true"
                            });
                        }
                        return Json(new {info = "", rpt = "Existe"});
                    }
                    if (aperturarRegistro.Id > 0)
                    {
                        _service.UpdateRegistroNotas(aperturarRegistro);
                        return Json(new
                        {
                            info =
                                PartialView(this, "_ListaRegistro",
                                    _service.GetRegistrosNotas("").OrderByDescending(a => a.Id)),
                            rpt = "true"
                        });
                    }

                }

                aperturarRegistro.nombreAño = anio.Anio;
                aperturarRegistro.AñoAcademicoId = anio.Id;
                return Json(new {info = PartialView(this, "Create", aperturarRegistro), rpt = "false"});

            }
            catch
            {
                return Json(new {info = "", rpt = "Existe"});
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

        public bool ComparaFechas(AperturarRegistroNotas aperturar)
        {
            var rpt = true;
            if (_service.GetRegistrosNotas("").Any(c => c.AñoAcademicoId.Equals(aperturar.AñoAcademicoId)))
            {
                var c = _service.GetRegistrosNotas("").Last(d => d.AñoAcademicoId.Equals(aperturar.AñoAcademicoId));
                if (c.FechaFinPrimTrim > aperturar.FechaInicioPrimTrim)
                {
                    rpt = false;
                    ModelState.AddModelError("FechaInicioPrimTrim",
                        "La fecha debe ser mayor a " + c.FechaFinPrimTrim.Date.ToString("D"));

                }
            }

            return rpt;
        }

        public bool ExiteRegistro(AperturarRegistroNotas aperturar)
        {
            return _service.GetRegistrosNotas("").
                Any(a => a.TrimestreId.
                    Equals(aperturar.TrimestreId) && a.AñoAcademicoId.Equals(aperturar.AñoAcademicoId));
        }

        // GET: ApRegNotas/Edit/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {


            var aperturarRegistro = _service.GetRegistroNotas(id);
            ViewBag.trimestre = new SelectList(_trimestre.GeTrimestres(), "Id", "Descripcion");
            var anio = _anio.GetAniosAcademicos("").Where(a => a.Id.Equals(aperturarRegistro.AñoAcademicoId)).Max();
            aperturarRegistro.nombreAño = anio.Anio;
            aperturarRegistro.AñoAcademicoId = anio.Id;

            return PartialView("Edit", aperturarRegistro);
        }

        // POST: ApRegNotas/Edit/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

    public ActionResult Edit(AperturarRegistroNotas registroNotas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateRegistroNotas(registroNotas);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ApRegNotas/Delete/5
        public ActionResult Delete(int id)
        {
            _service.GetRegistroNotas(id);
            return View();
        }

        // POST: ApRegNotas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AperturarRegistroNotas aperturarRegistro)
        {
            try
            {
                aperturarRegistro = _service.GetRegistroNotas(id);

                if (ModelState.IsValid)
                {
                    if (aperturarRegistro==null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        _service.DeleteRegistroNotas(id);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void Validaciones(AperturarRegistroNotas aperturar,string anio)
        {
            _validaciones.RequiredFechas("FechaInicioPrimTrim", aperturar.FechaInicioPrimTrim);
            _validaciones.RequiredFechas("FechaFinPrimTrim", aperturar.FechaFinPrimTrim);
            _validaciones.RequiredId("TrimestreId", aperturar.TrimestreId);


            _validaciones.VerificaRangos("FechaInicioPrimTrim", aperturar.FechaInicioPrimTrim, aperturar.FechaFinPrimTrim);

            _validaciones.VerificaFechaConAnioAcademico("FechaInicioPrimTrim", aperturar.FechaInicioPrimTrim,anio);
            _validaciones.VerificaFechaConAnioAcademico("FechaFinPrimTrim", aperturar.FechaFinPrimTrim,anio);
        }
    }
}
