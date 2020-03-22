using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSP.Utils;

namespace WSP.Service.SampleProcess
{
    public sealed class ReportProcess
    {
        public static ReportProcess Instance { get; }

        private static PDFReportGenerator _reportGenerator;

        static ReportProcess()
        {
            if(Instance == null)
            {
                Instance = new ReportProcess();

                _reportGenerator = new PDFReportGenerator();
               
            }
        }


        public async void Run()
        {
            GlobalStatistics.Instance.Jobs = new ConcurrentBag<IJob>();

            await Task.Factory.StartNew(() =>
            {
                #region Documnet Generation

                while (true)
                {
                    if (JobQueue.Instance.Count > 0)
                    {
                        var documentJob = JobQueue.Instance.Dequeue();
                        if (documentJob != null)
                        {
                            documentJob.State = JobState.Processing;

                            try
                            {
                                //Generate & Save PDF document
                                var pdfFile = _reportGenerator.Generate((SampleDocumentJob)documentJob);

                                //simulate long running job
                                long delay = LoremNET.Lorem.Number(1, 10) * 100;
                                System.Threading.Thread.Sleep((int)delay);
                                
                                documentJob.State = JobState.Completed;

                            }
                            catch (Exception ex)
                            {
                                documentJob.State = JobState.Failed;
                            }
                            //Attach and email
                            //Send Email
                        }
                    }

                }

                #endregion
            });
            
        }

        
    }
}
