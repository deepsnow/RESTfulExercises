using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeLogging.Startup))]
namespace TimeLogging
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
