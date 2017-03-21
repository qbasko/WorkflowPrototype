using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akkaNet
{
    public class Service1Actor : UntypedActor
    {

        public Service1Actor()
        {
            Console.WriteLine("service1Actor created");
        }
        protected override void OnReceive(object message)
        {
            Console.WriteLine("Service1 processing");
        }

    }
}
