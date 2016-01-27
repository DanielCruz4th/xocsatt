using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(xocsatt.WebApplication.Startup))]
namespace xocsatt.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
