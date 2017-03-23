using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FInalProject.Startup))]
namespace FInalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
