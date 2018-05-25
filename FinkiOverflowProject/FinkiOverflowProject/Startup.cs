using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinkiOverflowProject.Startup))]
namespace FinkiOverflowProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
