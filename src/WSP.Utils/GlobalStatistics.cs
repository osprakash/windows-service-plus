using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace WSP.Utils
{
    public sealed class GlobalStatistics
    {
        private static readonly GlobalStatistics instance = new GlobalStatistics();
        static GlobalStatistics()
        {
            
        }
        private GlobalStatistics()
        {
            Jobs = new ConcurrentBag<IJob>();
        }
        public static GlobalStatistics Instance
        {
            get
            {
                return instance;
            }
        }
        public long JobsQueued
        {
            get
            {
                return Jobs.Count(x => x.State == JobState.Queued);
            }
        }

        public long JobsCompleted
        {
            get
            {
                return Jobs.Count(x => x.State == JobState.Completed);
            }
        }


        public long JobsFailed
        {
            get
            {
                return Jobs.Count(x => x.State == JobState.Failed);
            }
        }

        public long JobsProcessing
        {
            get
            {
                return Jobs.Count(x => x.State == JobState.Processing);
            }
        }

        public ConcurrentBag<IJob> Jobs { get; set; }
    }
}
