using Akka.Actor;
using System;

namespace akkaNet
{
    public class Service2Actor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine("Service2 processing");
        }
    }
}
