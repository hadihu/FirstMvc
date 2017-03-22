using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstMvc.Startup))]
namespace FirstMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
