using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantFinder.Startup))]
namespace RestaurantFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
