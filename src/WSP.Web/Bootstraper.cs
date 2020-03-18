using Nancy;
using Nancy.Conventions;
using Nancy.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickerQ.Admin
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.ViewLocationConventions.Add(
                (viewName, model, context) => string.Concat("Web/", viewName));

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("js", "Web/js"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("assets", "Web/assets"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", "Web/fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("css", "Web/css"));

        }
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            /*enable lightningcache, vary by url params id,query,take and skip*/
            //this.EnableRapidCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, new[] { "query", "form", "accept" }); 
        }
    }
}
