using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheUsualJoints.Startup))]
namespace TheUsualJoints
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
