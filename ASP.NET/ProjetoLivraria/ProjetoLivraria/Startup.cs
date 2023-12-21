using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoLivraria.Startup))]
namespace ProjetoLivraria
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
