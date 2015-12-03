using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_SSA.Startup))]
namespace Project_SSA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
