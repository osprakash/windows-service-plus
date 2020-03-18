using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;
using Topshelf.Logging;
using System.Configuration;
using WSP.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WSP.Service
{
    public class SampleService : IDisposable
    {
        private static readonly LogWriter logger = HostLogger.Get<SampleService>();

        WebServer webServer;
        public SampleService(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<IConfiguration>();
            webServer = new WebServer(config);
        }
        
        public bool Start(HostControl hostControl)
        {
            logger.Info("Starting Web/API monitoring server...");
            webServer.Start();

            //logger.Info("Starting iPre Websocket listener");
            //WSClient.Instance.Listen();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            logger.Info("Stopping Web/API monitoring server...");
            webServer.Stop();

            logger.Info("Stopping iPre");
            //WSClient.Instance.Close();

            return true;
        }

         public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //WSClient.Instance.Close();
                webServer.Dispose();
            }
        }
    }
}
