using System.Web.Http;
using WebActivatorEx;
using Inlog.Frota.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Inlog.Frota.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "Inlog.Frota.Api");
                        c.IncludeXmlComments(GetXmlCommentsPath());

                        
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Exemplo de uso da Web API Frota");
                        c.DocExpansion(DocExpansion.List);
                    });
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Inlog.Frota.Api.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
