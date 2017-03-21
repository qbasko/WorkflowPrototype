using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akkaNet
{
    public enum FlowStateEnum
    {
        Starting = 0,
        CollectingDate,
        Computing,
        Storing,
        Closing,
        Initialized
    }
}
