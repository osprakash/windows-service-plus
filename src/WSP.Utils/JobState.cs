using System;
using System.Collections.Generic;
using System.Text;

namespace WSP.Utils
{
    public enum JobState
    {
        Queued = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3
    }
}
