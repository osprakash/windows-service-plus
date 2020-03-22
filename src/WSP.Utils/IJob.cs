using System;
using System.Collections.Generic;
using System.Text;

namespace WSP.Utils
{
    public interface IJob
    {
        Guid Id { get; set; }
        JobState State { get; set; }
    }
}
