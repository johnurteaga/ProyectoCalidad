using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JPCSystem.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Grados",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Grados", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Secciones",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Secciones", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Nivel",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Nivel", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Alumno",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Alumno", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "anio_Escolar",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AnioEscolar", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "apoderado",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Apoderado", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "aperturar",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ApRegNotas", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "asistencia",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Asistencia", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "curso",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Curso", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "docente",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Docente", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "horarios",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DocenteCurso", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "grado",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Grado", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "matricula",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Matricula", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "notas",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Notas", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "seccion",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Secciones", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
