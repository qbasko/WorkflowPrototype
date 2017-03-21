using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akkaNet
{
    class Program
    {
      
        static void Main(string[] args)
        {
           
            Console.WriteLine("Actor system created");

            Scheduler scheduler = new Scheduler();
            Console.ReadKey();



            ////       service1Ref.Tell("lol");

            //      var result =  service1Ref.Ask("lol");
            //      result.Wait();

            //       Console.ReadLine();
            //       System.Terminate();
        }
    }
}
