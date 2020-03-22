using System;
using System.Collections.Generic;
using System.Text;

namespace WSP.Utils
{
    public class JobQueue
    {
        private readonly Queue<IJob> queue = new Queue<IJob>();
        public event EventHandler Changed;
        protected virtual void OnChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }
        public virtual IJob Enqueue(IJob item)
        {
            item.Id = Guid.NewGuid();
            //Add it to InMemory job cache for stats and history
            GlobalStatistics.Instance.Jobs.Add(item);

            queue.Enqueue(item);
            OnChanged();
            return item;
        }
        public int Count { get { return queue.Count; } }

        public virtual IJob Dequeue()
        {
            IJob item = queue.Dequeue();
            OnChanged();
            return item;
        }

        public static JobQueue Instance { get; private set; }
        static JobQueue()
        {
            if(Instance == null)
            {
                Instance = new JobQueue();
            }
        }

    }
}
