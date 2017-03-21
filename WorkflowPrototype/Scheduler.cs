using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akkaNet
{
    public class Scheduler
    {
        private static ActorSystem System;

        public Scheduler()
        {
            System = ActorSystem.Create("SampleSystem");
            var loop = Observable.Interval(TimeSpan.FromSeconds(1.0));
            PrepareServices();

            loop.Subscribe(SpawnJob);
        }

        private void PrepareServices()
        {
            Props service1 = Props.Create<Service1Actor>();
            
            var actor = System.ActorOf(service1, "Service1Actor");
       
            Props service2 = Props.Create<Service2Actor>();
            System.ActorOf(service2, "Service2Actor");


        }

        public void SpawnJob(long i)
        {
            Props jobActor = Props.Create<JobActor>();
            string name = "jobActor" + i;


            IActorRef actorRef = System.ActorOf(jobActor, name);
            actorRef.Tell("Initialize");

        }
    }
}
