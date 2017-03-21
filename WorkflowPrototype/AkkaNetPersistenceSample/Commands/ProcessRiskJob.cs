using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample.Commands
{
    internal class ProcessRiskJob
    {
        public RiskJob RiskJob { get; }

        public ProcessRiskJob()
        {

        }

        public ProcessRiskJob(RiskJob riskJob)
        {
            RiskJob = riskJob;
        }
    }
}
