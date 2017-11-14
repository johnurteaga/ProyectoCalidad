using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using JPCSystem.Domain;
using JPCSystem.Service;
using JPCSystem.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using JPCSystem.Web.validaciones;

namespace JPCSystem.Web.Controllers
{
    public class AlumnoController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        private IAlumnoService _service;
        private IDocumentoService _DocumentoService;
        private IUbigeoService _ubigeo;
        private IUsuarioService _usuario;
        private readonly ValidacionesUsuario _validators;

        public AlumnoController(IAlumnoService alumnoService, IDocumentoService documentoService,
            IUsuarioService usuario, IUbigeoService ubigeo)
        {
            _service = alumnoService;
            _DocumentoService = documentoService;
            _ubigeo = ubigeo;
            _usuario = usuario;
            _validators = new ValidacionesUsuario(ModelState);
        }

        // GET: Alumno
        public ActionResult Index()
        {
            var lisDoc = _DocumentoService.GetDocumentos();
            ViewBag.documento = new SelectList(lisDoc, "Id", "NomDocumento");
            return View(_service.GetAlumnos(""));

        }

        [HttpPost]
        public ActionResult Index(String criterio)
        {
            var lista = _service.GetAlumnos(criterio);
            return PartialView("_AlumnoLista", lista);
        }


        // GET: Alumno/Details/5
        public ActionResult Details(int id)
        {
            var dato = _service.GetAlumno(id);
            var doc = _DocumentoService.GetDocumento(dato.DocumentoId);
            if (ModelState.IsValid)
            {
                ViewBag.documento = doc.NomDocumento;
            }
            return View(dato);
        }

        //[Authorize(Roles = "admin")]
        // GET: Alumno/Create
        [AllowAnonymous]
        public ActionResult Create()
        {

            Combo();
            return PartialView("Create");
        }

        public List<SelectListItem> Departamentos()
        {
            List<SelectListItem> DepartamentoId = new List<SelectListItem>();
            //var dato = _ubigeo.GetUbigeos().Where(u => u.IdUbigeo.Remove(0, 2).Equals("0000"));
            var dato = _ubigeo.GetUbigeos().Where(u => u.IdUbigeo.Equals("611001"));
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
            ViewBag.documento = _DocumentoService.GetDocumentos().OrderBy(d => d.NomDocumento);

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

        // POST: Alumno/Create
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Alumno alumno)
        {
            try
            {
                Combo();
                Validaciones(alumno);
                if (ModelState.IsValid) //validacion del si el modelo existe
                {
                    if (!_service.GetAlumnos("").Any(p => p.NumeroDocumento.Equals(alumno.NumeroDocumento)))
                        //si no hay alumnos regristrados
                    {
                        if (alumno.Id == 0)
                        {
                            alumno.Estado = true; //es nuevo
                            _service.AddAlumno(alumno); //agega el nuevo dato
                            AgregaUser(alumno);
                        }
                        if (alumno.Id > 0)
                        {
                            _service.UpdateAlumno(alumno);
                        }
                        return Json(new {data = "ok"});
                    }
                }
                return PartialView("Create", alumno);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public void AgregaUser(Alumno alumno)
        {
            var usr = new Usuario
            {
                AlumnoId = alumno.Id
            };
            _usuario.AddUsuario(usr);

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //primero se migra la base de datos y se copias esta parte de codigo
            if (!_context.Users.Any(u => u.UsuarioId.Equals(usr.id)))
            {
                var user = new ApplicationUser
                {
                    UserName = alumno.Correo,
                    Email = alumno.Correo,
                    UsuarioId = alumno.Id
                };
                var result = userManager.Create(user, "passw0rd");

                if (!_context.Roles.Any(r => r.Name.Equals("alumno")))
                {
                    _context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() {Name = "alumno"});
                    _context.SaveChanges();
                    userManager.AddToRole(user.Id, "alumno");
                }
                else
                {
                    userManager.AddToRole(user.Id, "alumno");
                }
            }
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        // GET: Alumno/Edit/5
        public ActionResult Edit(int id)
        {
            Combo();
            var lista = _service.GetAlumno(id);
            return PartialView("Edit", lista);
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult Edit(Alumno alumno)
        {
            Combo();
            try
            {
                Validaciones(alumno);
                if (ModelState.IsValid) //validacion del si el modelo existe
                {

                    if (!_service.GetAlumnos("").Any(p => p.NumeroDocumento.Equals(alumno.NumeroDocumento)))
                    {
                        _service.AddAlumno(alumno); //agega el nuevo dato
                        AgregaUser(alumno);
                        return RedirectToAction("Index");
                    }
                    return Json("Error");

                }
                return PartialView(alumno);

            }
            catch
            {
                return View();
            }
        }

        // GET: Alumno/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.documento = _DocumentoService.GetDocumentos().OrderBy(d => d.NomDocumento);
            return View(_service.GetAlumno(id));
        }

        // POST: Alumno/Delete/5
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]

    public ActionResult Delete(int id, Alumno alumno)
        {
            try
            {
               alumno = _service.GetAlumno(id);

                if (!ModelState.IsValid)
                {
                   
                    if (alumno == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        _service.DeleteAlumno(id);
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
            if (departmentId.ToString().Count() < 24 && departmentId.ToString().Any()) id = (0 + "" + departmentId);
            
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
           if (provinciaId.ToString().Count() < 24 && provinciaId.ToString().Count() > 0) id = (0 + "" + provinciaId);
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


        public void Validaciones(Alumno alumno)
        {
            _validators.Required("Nombre", alumno.Nombre);
            _validators.MaxLength("Nombre", alumno.Nombre,50);
            _validators.Required("ApMaterno", alumno.ApMaterno);
            _validators.MaxLength("ApMaterno", alumno.ApMaterno,50);
            _validators.Required("ApPaterno", alumno.ApPaterno);
            _validators.MaxLength("ApPaterno", alumno.ApPaterno,50);
            _validators.Required("Genero", alumno.Genero);
            _validators.Required("Direccion", alumno.Direccion);
            _validators.MaxLength("Direccion", alumno.Direccion,50);
            _validators.Required("Correo", alumno.Correo);
            _validators.MaxLength("Correo", alumno.Correo,50);
            _validators.Email("Correo", alumno.Correo);
            _validators.RequiredNro("Telefono", alumno.Telefono);
            _validators.RequiredNro("NumeroDocumento", alumno.NumeroDocumento);
            var numeroDoc = alumno.NumeroDocumento.ToString();
            _validators.MaxLengthDocumento("NumeroDocumento", numeroDoc, 8);
            _validators.RequiredFechas("FechaNacimiento", alumno.FechaNacimiento);
            _validators.FechaMayor("FechaNacimiento", alumno.FechaNacimiento);
            _validators.RequiredId("DocumentoId", alumno.DocumentoId);
            _validators.Required("IdUbigeo", alumno.IdUbigeo);

        }

    }
}
