using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UMS.Web.Startup))]
namespace UMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
