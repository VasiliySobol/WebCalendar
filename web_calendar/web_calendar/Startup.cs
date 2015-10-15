using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web_calendar.Startup))]
namespace web_calendar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
