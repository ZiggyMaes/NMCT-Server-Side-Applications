using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReceptenApp.Startup))]
namespace ReceptenApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
