using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Microsoft.Practices.Unity;
using MobileWebAPI.Common;
using MobileWebAPI.Identity;
using BackendService.Model;
using Application.DTO;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(MobileWebAPI.Startup))]

namespace MobileWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            ConfigureAuth(app);
            
            var config = new HttpConfiguration();

            Register(config);

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            ConfigureWebApi(config);
            
            app.UseWebApi(config);

        }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions();
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            //container.RegisterType<ILoginProvider, LocalUserLoginProvider>(new HierarchicalLifetimeManager());
            container.RegisterInstance<ILoginProvider>(new DomanUserLoginProvider("Hitachi"));
            container.RegisterType<IRepository<Team> , Repository<Team> >(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Other Web API configuration not shown.
        }
    }
}
