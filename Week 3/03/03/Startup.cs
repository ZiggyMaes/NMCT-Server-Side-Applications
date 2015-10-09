using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_03.Startup))]
namespace _03
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
