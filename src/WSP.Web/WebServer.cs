using Nancy.Hosting.Self;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using WSP.Utils;

namespace WSP.Admin
{
    public class WebServer : IDisposable
    {
        private IConfiguration _configuration;
        public WebServer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private NancyHost m_nancyHost;

        public void Start()
        {
            var port = _configuration["WebSitePort"];

            var url = "http://localhost:" + port;
            //API / Web server
            m_nancyHost = new NancyHost(new Uri(url));
            m_nancyHost.Start();

            //StatsCounter.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //string stats = JsonConvert.SerializeObject(StatsCounter.Instance);

            //var context = GlobalHost.ConnectionManager.GetHubContext<StatsHub>();
            //context.Clients.All.broadcastMessage("hi");      
        }

        public void Stop()
        {
            m_nancyHost.Stop();
            //m_socketServer.Dispose();
            //Console.WriteLine("Stopped. Good bye!");
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
                m_nancyHost.Dispose();
            }
        }
    }
}
