using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JPCSystem.Web.Startup))]
namespace JPCSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
