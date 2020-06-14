using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(autentikasManager.Startup))]
namespace autentikasManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
