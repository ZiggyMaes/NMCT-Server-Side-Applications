using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_02.Startup))]
namespace _02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
