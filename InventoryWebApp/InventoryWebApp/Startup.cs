using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryWebApp.Startup))]
namespace InventoryWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
