using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample.Events
{
    internal class RiskJobProcessed
    {
        public RiskJob RiskJob { get; }

        public RiskJobProcessed(RiskJob riskJob)
        {
            RiskJob = riskJob;
        }
    }
}
