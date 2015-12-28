using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Recepten.Startup))]
namespace Recepten
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
