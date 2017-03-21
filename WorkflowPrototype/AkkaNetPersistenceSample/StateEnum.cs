using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample
{
    public enum StateEnum
    {
        PreProcessing,
        Starting,
        Processing,
        PostProcessing,
        Finished
    }
}
