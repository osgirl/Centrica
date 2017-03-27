using System.Web.Http;
using FibancciService.App_Start;
using Owin;

namespace FibancciService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appbuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            appbuilder.UseWebApi(httpConfiguration);
        }
    }
}
