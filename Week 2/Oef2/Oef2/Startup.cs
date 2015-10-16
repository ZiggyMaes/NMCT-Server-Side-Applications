using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Oef2.Startup))]
namespace Oef2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
