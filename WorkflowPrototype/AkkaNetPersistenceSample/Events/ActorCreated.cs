using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample.Events
{
    internal class ActorCreated
    {
        public string ActorName { get; }

        public ActorCreated(string actorName)
        {
            ActorName = actorName;
        }
    }
}
