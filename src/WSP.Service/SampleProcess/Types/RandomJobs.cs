using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSP.Utils;

namespace WSP.Service.SampleProcess
{
    public class RandomJobs
    {
        public async void CreateAndQueue()
        {
            while (true)
            {
                //creates random job for demo every 10s
                await Task.Delay(10000).ContinueWith((t) =>
                {
                    var documentJob = RandomJob();
                    documentJob.State = JobState.Queued;

                    JobQueue.Instance.Enqueue(documentJob);

                });

            }
        }

        private SampleDocumentJob RandomJob()
        {
            //Prepare sample document job
            var documentInfo = new SampleDocumentJob();
            documentInfo.Title = LoremNET.Lorem.Words(5, 8);
            documentInfo.Description = LoremNET.Lorem.Words(5, 40);

            documentInfo.Data = new List<string>();
            for (int i = 0; i < LoremNET.Lorem.Number(5, 10); i++)
            {
                documentInfo.Data.Add(LoremNET.Lorem.Words(5, 8));
            }

            documentInfo.ReceipentEmail = LoremNET.Lorem.Email();

            return documentInfo;
        }
    }
}
