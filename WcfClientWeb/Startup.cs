using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WcfClientWeb.Startup))]
namespace WcfClientWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
