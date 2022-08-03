using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Compactor.Startup))]
namespace Compactor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
