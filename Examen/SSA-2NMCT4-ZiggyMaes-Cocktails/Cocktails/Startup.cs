using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cocktails.Startup))]
namespace Cocktails
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
