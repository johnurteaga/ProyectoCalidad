using JPCSystem.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JPCSystem.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JPCSystem.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JPCSystem.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var userStore = new UserStore<ApplicationUser>(context); //almacen de ususarios
            var userManager = new UserManager<ApplicationUser>(userStore);//getor de Usuarios

            if (!context.Users.Any(u => u.UserName.Equals("admin@jpc.pe")))
            {
                var user = new ApplicationUser()
                {
                    UserName = "admin@jpc.pe",
                    Email = "admin@jpc.pe",

                };
                userManager.Create(user, "passw0rd");
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "admin" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "admin");
            }
        }
    }
}
