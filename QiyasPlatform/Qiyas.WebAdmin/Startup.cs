using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Qiyas.WebAdmin.Startup))]
namespace Qiyas.WebAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
