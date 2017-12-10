using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using Swagger.Net;

//[assembly: WebActivator.PreApplicationStartMethod(typeof(WebAppDms.App_Start.SwaggerNet), "PreStart")]
//[assembly: WebActivator.PostApplicationStartMethod(typeof(WebAppDms.App_Start.SwaggerNet), "PostStart")]
namespace WebAppDms.App_Start 
{
    public static class SwaggerNet 
    {
        public static void PreStart() 
        {
            RouteTable.Routes.MapHttpRoute(
                name: "SwaggerApi",
                routeTemplate: "api/docs/{controller}",
                defaults: new { swagger = true }
            );            
        }
        
        public static void PostStart() 
        {
            var config = GlobalConfiguration.Configuration;

            config.Filters.Add(new SwaggerActionFilter());
            
            try
            {
                config.Services.Replace(typeof(IDocumentationProvider),
                    new XmlCommentDocumentationProvider(HttpContext.Current.Server.MapPath("~/bin/WebAppDms.XML")));
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Please enable \"XML documentation file\" in project properties with default (bin\\WebAppDms.XML) value or edit value in App_Start\\SwaggerNet.cs");
            }
        }
    }
}