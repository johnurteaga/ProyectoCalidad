using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using JPCSystem.Web.validaciones;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace JPCSystem.Web.Controllers
{
    public class DocenteController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        private IDocenteService _service;
        private IDocumentoService _DocumentoService;
        private IUbigeoService _ubigeo;
        private IUsuarioService _usuario;
        private readonly ValidacionesUsuario _validators;

        public DocenteController(IDocenteService service, IDocumentoService documentoService,
            IUbigeoService ubigeo, IUsuarioService user)
        {
            _service = service;
            _DocumentoService = documentoService;
            _ubigeo = ubigeo;
            _validators = new ValidacionesUsuario(ModelState);
            //_usuario = user;
        }

        // GET: Docente
        public ActionResult Index()
        {
            ViewBag.FechaActual = (int) (DateTime.Now.Year);
            var lista = _service.GetDocentes("");
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(string criterio)
        {
            var lista = _service.GetDocentes(criterio);
            return PartialView("_DocenteLista", lista);
        }


        // GET: Docente/Details/5
        public ActionResult Details(int id)
        {
            var lista = _service.GetDocente(id);
            return PartialView("Details", lista);
        }

        // GET: Docente/Create
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Create()
        {
            CargaCombos();
            return PartialView("Create");
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

        // POST: Docente/Create
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Create(Docente docente)
        {
            try
            {
                Validaciones(docente);
                CargaCombos();
                if (ModelState.IsValid)
                {

                    if (docente.Id == 0)
                    {
                        if (ExisteDocente(docente))
                        {
                            _service.AddDocente(docente); //agega el nuevo dato
                            AgregarUser(docente);
                            return
                                Json(
                                    new
                                    {
                                        info = PartialView(this, "_DocenteLista", _service.GetDocentes("")),
                                        rpt = "true"
                                    });
                        }
                        return Json(new {info = "", rpt = "Existe"});
                    }
                    if (docente.Id > 0)
                    {
                        _service.UpdateDocente(docente);
                        return Json(new
                        {
                            info = PartialView(this, "_DocenteLista", _service.GetDocentes("")),
                            rpt = "true"
                        });
                    }
                }
                return Json(new {info = PartialView(this, "Create", docente), rpt = "false"});
            }

            catch
            {
                return View();
            }
        }

        public bool ExisteDocente(Docente docente)
        {
            if (!_service.GetDocentes("").Any(p => p.NroDocumento.Equals(docente.NroDocumento)))
                return true;

            return false;
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


        private void AgregarUser(Docente docente)
        {
            var usr = new Usuario
            {
                DocenteId = docente.Id
            };
            _usuario.AddUsuario(usr);

            var userManager = HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
                //primero se migra la base de datos y se copias esta parte de codigo
            if (!_context.Users.Any(u => u.UsuarioId.Equals(usr.id)))
            {
                var user = new ApplicationUser
                {
                    UserName = docente.Nombres,
                    Email = docente.Nombres + "@jpc.com",
                    UsuarioId = docente.Id
                };
                var result = userManager.Create(user, "passw0rd");

                if (!_context.Roles.Any(r => r.Name.Equals("docente")))
                {
                    _context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() {Name = "docente"});
                    _context.SaveChanges();
                    userManager.AddToRole(user.Id, "decente");
                }
                else
                {
                    userManager.AddToRole(user.Id, "docente");
                }
            }
        }

        // GET: Docente/Edit/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

    public ActionResult Edit(int id)
        {
            CargaCombos();
            var datos = _service.GetDocente(id);
            return PartialView("Edit",datos);
        }

        // POST: Docente/Edit/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Edit(Docente docente)
        {
            try
            {
                CargaCombos();
                if (ModelState.IsValid)
                {
                    _service.UpdateDocente(docente);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Docente/Delete/5
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            return View(_service.GetDocente(id));
        }

        // POST: Docente/Delete/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Delete(int id, Docente docente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    docente = _service.GetDocente(id);

                    if (docente == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        _service.DeleteDocente(id);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void CargaCombos()
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

        public void Validaciones(Docente docente)
        {
            _validators.Required("Nombres", docente.Nombres);
            _validators.MaxLength("Nombres", docente.Nombres, 50);
            _validators.Required("ApMaterno", docente.ApMaterno);
            _validators.MaxLength("ApMaterno", docente.ApMaterno, 50);
            _validators.Required("ApPaterno", docente.ApPaterno);
            _validators.MaxLength("ApPaterno", docente.ApPaterno, 50);
            _validators.Required("Genero", docente.Genero);
            _validators.Required("EstadoCivil", docente.EstadoCivil);
            _validators.Required("Direccion", docente.Direccion);
            _validators.MaxLength("Direccion", docente.Direccion, 50);
            _validators.RequiredNro("Telefono", docente.Telefono);
            _validators.RequiredNro("AniosExperiencia", docente.AniosExperiencia);
            _validators.RequiredNro("NroDocumento", docente.NroDocumento);
            _validators.RequiredFechas("FechaNacimiento", docente.FechaNacimiento);
           // _validators.FechaMayor("FechaNacimiento", docente.FechaNacimiento);
            _validators.RequiredId("DocumentoId", docente.DocumentoId);
            _validators.Required("IdUbigeo", docente.IdUbigeo);

        }
    }
}


