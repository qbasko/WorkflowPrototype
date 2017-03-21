using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample.Commands
{
    internal class CreateActor
    {
        public string ActorName { get; }

        public CreateActor(string actorName)
        {
            ActorName = actorName;
        }
    }
}
