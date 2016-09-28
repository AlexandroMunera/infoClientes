using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(infoClientes.Startup))]
namespace infoClientes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
