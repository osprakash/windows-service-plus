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
            Get("/", args => View["index"]);
            Get("/index", args => View["index"]);
            Get("/logs", args => View["logs"]);
            Get("/health", args => View["health"]);
            Get("/theme", args => View["theme"]);
        }
    }
}
