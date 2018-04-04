using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaLoja.Startup))]
namespace SistemaLoja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
