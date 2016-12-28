using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.OData.Builder;
using MyLabProject.Models.db.people.model;
using System.Web.OData.Extensions;
using MyLabProject.App_Start;

namespace MyLabProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("People");
            builder.EntitySet<PersonDynamics>("PersonDynamics");

            builder.EntityType<Person>().Collection.Function("GetPeopleFunction").ReturnsFromEntitySet<Person>("People");
            //builder.Function("GetPeopleFunction").ReturnsFromEntitySet<Person>("Person");

            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "TestController" },
                constraints: null,
                handler: new SpecialHandler(config)
            );
        }
    }
}
