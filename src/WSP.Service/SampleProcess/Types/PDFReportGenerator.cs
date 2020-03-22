using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using WSP.Utils;

namespace WSP.Service.SampleProcess
{
    public class PDFReportGenerator
    {
        public PDFReportGenerator()
        {

        }

        public async Task<string> Generate(SampleDocumentJob info)
        {
            await Task.Delay(5000);
            return "";
        }
    }
}
