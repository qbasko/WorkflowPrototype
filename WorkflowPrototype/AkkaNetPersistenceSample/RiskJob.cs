using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample
{
    public class RiskJob
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public StateEnum State { get; set; }

        public virtual void SetNextState()
        {
            State += 1;
        }
    }
}
