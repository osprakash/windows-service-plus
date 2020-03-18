using System;
using System.Collections.Generic;
using System.Text;

namespace WSP.Utils
{
    public sealed class GlobalConfiguration
    {
        public static GlobalConfiguration Instance
        {
            get;
        }
        static GlobalConfiguration()
        {
            if(Instance == null)
            {
                Instance = new GlobalConfiguration();
            }
        }

        public string WebSiteVirtualPath { get; set; }
    }
}
