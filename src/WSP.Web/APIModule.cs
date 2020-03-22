using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using System.Linq;
using WSP.Utils;

namespace WSP.Web
{
    public class APIModule : NancyModule
    {
        public APIModule()
        {
            Get("/api/stats", args =>
            {
                string stats = JsonConvert.SerializeObject(GlobalStatistics.Instance);
                return Response.AsText(stats);
            });


            //Add new job to the queue
            Post("/api/jobs", args =>
            {
                string requestBody = Request.Body.AsString();
                SampleDocumentJob documentJob = JsonConvert.DeserializeObject<SampleDocumentJob>(requestBody);

                var job = JobQueue.Instance.Enqueue(documentJob);

                var result = new {
                    id = job.Id,
                    state = job.State
                };
                return Response.AsText(JsonConvert.SerializeObject(result));
            });

            //gets specific job info
            Get("/api/jobs/{id}", args =>
            {
                IJob documentJob = GlobalStatistics.Instance.Jobs.FirstOrDefault(x => x.Id == Guid.Parse(args.id));
                if(documentJob != null)
                {
                    return Response.AsText(JsonConvert.SerializeObject(documentJob));
                }
                return Response.AsText("Job not found.");
            });

        }
    }
}
