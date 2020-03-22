using System;
using System.Collections.Generic;
using System.Text;

namespace WSP.Utils
{
    public class SampleDocumentJob : IJob
    {
        public JobState State { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Data { get; set; }

        public string ReceipentEmail { get; set; }
        public Guid Id { get; set; }
    }


}
