using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrundtvigsDyreklinik.Startup))]
namespace GrundtvigsDyreklinik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
