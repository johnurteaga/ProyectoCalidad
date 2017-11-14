using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class ApoderadoController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        private IApoderadoService _service;
        private IDocumentoService _documentoService;
        private IUbigeoService _ubigeo;
        private readonly ValidacionesUsuario _validators;


        public ApoderadoController(IApoderadoService service, IDocumentoService documentoService, IUbigeoService ubigeo)
        {
            _service = service;
            _documentoService = documentoService;
            _ubigeo = ubigeo;
            _validators = new ValidacionesUsuario(ModelState);
        }

        // GET: Apoderado
        public ActionResult Index()
        {
            var lista = _service.GetApoderados("");
            return View(lista);

        }

        [HttpPost]
        public ActionResult Index(String criterio)
        {
            try
            {
                var lista = _service.GetApoderados(criterio);
                return View(lista);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }

        }

        // GET: Apoderado/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var lista = _service.GetApoderado(id);
                return View(lista);
            }
            catch (Exception)
            {
                return View();
            }
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        // GET: Apoderado/Create
        public ActionResult Create()
        {
            Combo();
            return View("Create");
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

        public void Combo()
        {
            ViewBag.documento = _documentoService.GetDocumentos().OrderBy(d => d.NomDocumento);

            var DepartamentoId = Departamentos();
            ViewBag.departamento = new SelectList(DepartamentoId, "Text", "Value");

            var ProvinciaId = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Seleccione", Value = "Seleccione"}
            };
            ViewBag.provincia = new SelectList(ProvinciaId, "Text", "Value");

            var DistritoId = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Seleccione", Value = "Seleccione"}
            };
            ViewBag.distrito = new SelectList(DistritoId, "Text", "Value");
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        // POST: Apoderado/Create
        [HttpPost]
        public ActionResult Create(Apoderado apoderado)
        {
            Validaciones(apoderado);
            try
            {
                Combo();


                if (ModelState.IsValid)
                {
                    _service.AddApoderado(apoderado);
                    return View("Index", _service.GetApoderados(""));
                }

                return View("Create", apoderado);
            }
            catch
            {
                return View();
            }
        }

        // GET: Apoderado/Edit/5
        public ActionResult Edit(int id)
        {
            // ViewBag.documento = _documentoService.GetDocumentos();
            Combo();

            return View(_service.GetApoderado(id));
        }

        // POST: Apoderado/Edit/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Edit(Apoderado apoderado)
        {
            // ViewBag.documento = _documentoService.GetDocumentos();
            Combo();
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateApoderado(apoderado);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void Validaciones(Apoderado apoderado)
        {
            _validators.Required("Nombres", apoderado.Nombres);
            _validators.MaxLength("Nombres", apoderado.Nombres, 50);
            _validators.Required("ApMaterno", apoderado.ApMaterno);
            _validators.MaxLength("ApMaterno", apoderado.ApMaterno, 50);
            _validators.Required("ApPaterno", apoderado.ApPaterno);
            _validators.MaxLength("ApPaterno", apoderado.ApPaterno, 50);
            _validators.Required("Genero", apoderado.Genero);
            _validators.Required("Direccion", apoderado.Direccion);
            _validators.MaxLength("Direccion", apoderado.Direccion, 50);
            _validators.Required("Correo", apoderado.Correo);
            _validators.MaxLength("Correo", apoderado.Correo, 50);
            _validators.Email("Correo", apoderado.Correo);
            _validators.RequiredNro("Telefono", apoderado.Telefono);
            _validators.RequiredNro("NroDocumento", apoderado.NroDocumento);
            var numeroDoc = apoderado.NroDocumento.ToString();
            _validators.MaxLengthDocumento("NroDocumento", numeroDoc, 8);
            _validators.RequiredFechas("FechaNacimiento", apoderado.FechaNacimiento);
            //_validators.FechaMayor("FechaNacimiento", apoderado.FechaNacimiento);
            _validators.RequiredId("DocumentoId", apoderado.DocumentoId);
            _validators.Required("IdUbigeo", apoderado.IdUbigeo);

        }

        // GET: Apoderado/Delete/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

    public ActionResult Delete(int id)
        {
            return View(_service.GetApoderado(id));
        }

        // POST: Apoderado/Delete/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Delete(int id, Apoderado apoderado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    apoderado = _service.GetApoderado(id);

                    if (apoderado==null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        _service.DeleteApoderado(id);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetProvincias(int departmentId)
        {

            string id = "";
            if (departmentId.ToString().Count() < 6 && departmentId.ToString().Any()) id = (0 + "" + departmentId);
            else id = departmentId.ToString();

            _context.Configuration.ProxyCreationEnabled = false;
            var provincias = _ubigeo.GetUbigeos().Where(u => (u.IdUbigeo.ToString().Remove(0, 4).ToString() == "00") &&
                                                             (u.IdUbigeo.ToString().Remove(2, 4).Equals(id.Remove(2, 4)))).
                OrderBy(u => u.Provincia);

            var provincia = new List<Ubigeo>();

            for (int i = 1; i < provincias.Count(); i++)
            {
                provincia.Add(provincias.ToList()[i]);
            }
            return Json(provincia);
        }

        public JsonResult GetDistritos(int provinciaId)
        {

            string id = "";
            if (provinciaId.ToString().Count() < 6 && provinciaId.ToString().Count() > 0) id = (0 + "" + provinciaId);
            else { id = provinciaId.ToString(); }

            _context.Configuration.ProxyCreationEnabled = false;
            var Distritos = _ubigeo.GetUbigeos().Where(p => p.IdUbigeo.ToString().Remove(0, 4) != "00" &&
                                                            p.IdUbigeo.ToString().Remove(4, 2).Equals(id.Remove(4, 2)));
            var distrito = new List<Ubigeo>();

            for (int i = 0; i < Distritos.Count(); i++)
            {
                distrito.Add(Distritos.ToList()[i]);
            }
            return Json(distrito);

        }


    }
}
