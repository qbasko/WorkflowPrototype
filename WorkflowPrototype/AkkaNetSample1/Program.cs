using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetSample1
{
    public class Greet
    {
        public Greet(string who)
        {
            Who = who;
        }
        public string Who { get; private set; }
    }

    public class ActorA : ReceiveActor
    {
        public ActorA()
        {
            Receive<Greet>(greet =>
               Console.WriteLine("ActorA. Hello {0}", greet.Who));
        }
    }

    public class ActorB : ReceiveActor
    {
        public ActorB()
        {
            Receive<Greet>(greet =>
               Console.WriteLine("ActorB. Hello {0}", greet.Who));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new actor system (a container for your actors)
            var system = ActorSystem.Create("MySystem");

            // Create your actor and get a reference to it.
            // This will be an "ActorRef", which is not a
            // reference to the actual actor instance
            // but rather a client or proxy to it.


            var actorA = system.ActorOf<ActorA>("actorA");
            var actorB = system.ActorOf<ActorB>("actorB");

            while (true)
            {
                var rl = Console.ReadLine();
                if (rl == "a")
                {
                    actorA.Tell(new Greet("Actor A"));
                }
                if (rl == "b")
                {
                    actorB.Tell(new Greet("Actor B"));
                }
                if (rl == "x")
                    break;
            }


            // This prevents the app from exiting
            // before the async work is done
            Console.ReadLine();
            system.Terminate();
        }
    }
}

