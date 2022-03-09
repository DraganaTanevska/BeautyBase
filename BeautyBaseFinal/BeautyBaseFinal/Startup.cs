using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeautyBaseFinal.Startup))]
namespace BeautyBaseFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
