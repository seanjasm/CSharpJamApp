using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharpJamApp.Startup))]
namespace CSharpJamApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
