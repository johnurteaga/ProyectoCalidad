using JPCSystem.Service;
using JPCSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using JPCSystem.Domain;
using JPCSystem.Web.validaciones;
using Microsoft.Ajax.Utilities;

namespace JPCSystem.Web.Controllers
{
    public class GradosController : Controller
    {
        private readonly ValidacionesDeGestionarAulas _validators;
        private ApplicationDbContext _context = new ApplicationDbContext();

        private IGradoService _service;
        private INivelService _nivel;

        public GradosController(IGradoService service,INivelService nivel)
        {
            _service = service;
            _nivel = nivel;
            _validators = new ValidacionesDeGestionarAulas(ModelState,service);
        }

        // GET: Grados

        public ActionResult Index()
        {
            ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            return View(_service.GetGrados().OrderBy(a=> a.Nivel.NombreNivel.ToUpper().Equals("PRIMARIA")));
        }


        public void Validaciones(Grado grado)
        {
            _validators.RequiredNombre("NombreGrado", grado.NombreGrado);
            _validators.RequiredId("NivelId", grado.NivelId);
           // _validators.RetipeNombreDeGradoYNivel("NombreGrado",grado.NombreGrado,grado.NivelId);

        }
        // GET: Grados
        [HttpPost]
        public ActionResult Index(String criterio)
        {
            var lista = criterio != ""?
                _service.GetGrados().Where(g => g.NombreGrado.ToUpper().Contains(criterio.ToUpper()) || 
                g.Nivel.NombreNivel.ToUpper().Contains(criterio.ToUpper()))
                : _service.GetGrados();
            return PartialView("_Index",lista); 
        }
        // GET: Grados/Create
        public ActionResult Create()
        {
            ViewBag.niveles = new SelectList(_nivel.GetNiveles(),"Id", "NombreNivel");
            return PartialView("_Create");
        }

        // POST: Grados/Create
        [HttpPost]
        public ActionResult Create(Grado grado,int id)
        {
            Validaciones(grado);
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        if (!_service.GetGrados().Any(g => g.NombreGrado.Equals(grado.NombreGrado) &&
                                                           g.NivelId.Equals(grado.NivelId)))
                        {
                            _service.AddGrado(grado);
                            ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
                        }
                        else
                        {
                            var rpt = "false";
                            ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
                            return Json(rpt);
                        }
                    }
                    

                    else
                    {
                        _service.UpdateGrado(grado);
                    }
                }
               
                return PartialView("_Index",_service.GetGrados());
            }
            catch (Exception e)
            {
                return View("Index");
            }
        }

        // GET: Grados/Edit/5
        public ActionResult Edit(int id)
        {
            var grado = _service.GetGrado(id);
            ViewBag.niveles = new SelectList(_nivel.GetNiveles(), "Id", "NombreNivel");
            return PartialView("_Create",grado);
        }
    }
}