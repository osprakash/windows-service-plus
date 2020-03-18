using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSP.Utils;

namespace TickerQ.Admin
{
    public class WebModule : NancyModule
    {
        public WebModule()
        {
            //Get["/"] = parameters =>
            //{
            //    var feeds = new string[] { "foo", "bar" };
            //    return Response.AsJson(feeds);
            //};
            //Get("/", args => "Hello World");
            
            Get("/", args => View["index", GlobalConfiguration.Instance.WebSiteVirtualPath]);
        }
    }
}
