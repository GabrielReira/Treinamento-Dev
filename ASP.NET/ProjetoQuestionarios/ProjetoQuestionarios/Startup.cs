using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoQuestionarios.Startup))]
namespace ProjetoQuestionarios
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
