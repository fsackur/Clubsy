using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clubsy.Startup))]
namespace Clubsy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
