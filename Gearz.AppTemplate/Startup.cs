using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gearz.Startup))]
namespace Gearz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
