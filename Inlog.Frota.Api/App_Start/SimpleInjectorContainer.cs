using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.WebApi;
using Inlog.Frota.Service.Interface;
using Inlog.Frota.Service;
using Inlog.Frota.DAL.Interface.Repositories;
using Inlog.Frota.DAL.Repositories;

namespace Inlog.Frota.Api.App_Start
{
    public static class SimpleInjectorContainer
    {
        public static void RegisterServices()
        {

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IVeiculoRepository, VeiculoRepository>(Lifestyle.Scoped);
            container.Register<IVeiculoService, VeiculoService>(Lifestyle.Scoped);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);


            container.Verify();

            //return container;


        }
    }
}