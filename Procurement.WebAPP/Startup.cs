using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Procurement.WebAPP.Startup))]
namespace Procurement.WebAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
