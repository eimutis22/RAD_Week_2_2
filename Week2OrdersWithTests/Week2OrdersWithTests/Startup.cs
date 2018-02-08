using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week2OrdersWithTests.Startup))]
namespace Week2OrdersWithTests
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
