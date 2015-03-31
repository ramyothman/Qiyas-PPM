using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Qiyas.Web.Startup))]
namespace Qiyas.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
