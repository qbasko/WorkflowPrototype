using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akkaNet
{
    public class JobActor : UntypedActor
    {
        private FlowStateEnum State;
        private int ID;
        public JobActor()
        {
            State = FlowStateEnum.Initialized;
            Become(Ready);
        }

        protected override void OnReceive(object message)
        {
        }

        private void Ready(object message)
        {
            //initialize stuff
            if (State == FlowStateEnum.Initialized)
            {
                State = FlowStateEnum.Starting;
                ID = new Random().Next(1000);
                // Do my stuff; and then:
                var actor = GetNextStep();
                actor.Tell(message);
            }
        }

        private ActorSelection GetNextStep()
        {
            if (new Random().Next(10) % 2 == 0)
            {
                return Context.ActorSelection("akka://SampleSystem/user/Service1Actor");
            }

            return Context.ActorSelection("akka://SampleSystem/user/Service2Actor");

        }




    }
}
