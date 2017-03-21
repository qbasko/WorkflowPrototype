using Akka.Actor;
using AkkaNetPersistenceSample.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
using AkkaNetPersistenceSample.Commands;

namespace AkkaNetPersistenceSample
{
    class Program
    {
        private static ActorSystem _system { get; set; }
        private static IActorRef _scheduler { get; set; }

        static void Main(string[] args)
        {
            _system = ActorSystem.Create("WFSystem");
            _scheduler = _system.ActorOf<Scheduler>("Scheduler");
            ForegroundColor = ConsoleColor.White;
            DisplayInstructions();

            while (true)
            {
                Thread.Sleep(2000);
                ForegroundColor = ConsoleColor.White;

                var action = ReadLine();

                var actorName = action.Split(' ')[0];

                if (action.Contains("create1"))
                {
                    CreateBlock1();
                }
                else if (action.Contains("display1"))
                {
                    DisplayActor();
                }
                else if (action.Contains("display2"))
                {
                    DisplayActor2();
                }
                else if (action.Contains("error1"))
                {
                    ErrorPlayer();
                }
                else if (action.Contains("process1"))
                {
                    ProcessRiskJob();
                }
                else if (action.Contains("create2"))
                { 
                    CreateBlock2();
                }
                else if (action.Contains("error2"))
                {
                    ErrorPlayer2();
                }
                else if (action.Contains("process2"))
                {
                    ProcessRiskJob2();
                }
                else
                {
                    WriteLine("Unknown command");
                }
            }
        }

        private static void DisplayInstructions()
        {
            Thread.Sleep(2000);
            ForegroundColor = ConsoleColor.White;

            WriteLine("Available commands:");
            WriteLine("create1 - create block1");
            WriteLine("display1 - display block1");
            WriteLine("error1 - throw exception in block1");
            WriteLine("process1  - process block1");
            WriteLine("create2 - create block1");
            WriteLine("display2 - display block1");
            WriteLine("error2 - throw exception in block1");
            WriteLine("process2  - process block1");
        }

        private static void CreateBlock1()
        {
            _scheduler.Tell(new CreateActor("Block1"));
        }

        private static void CreateBlock2()
        {
            var actor = _system.ActorSelection("/user/Scheduler/Block1");
            actor.Tell(new CreateActor("Block2"));
        }

        private static void ProcessRiskJob()
        {
            _system.ActorSelection("/user/Scheduler/Block1")
                  .Tell(new ProcessRiskJob());
        }

        private static void ProcessRiskJob2()
        {
            _system.ActorSelection("/user/Scheduler/Block1/Block2")
                  .Tell(new ProcessRiskJob());
        }

        private static void ErrorPlayer()
        {
            _system.ActorSelection("/user/Scheduler/Block1")
                  .Tell(new SimulateError());
        }

        private static void ErrorPlayer2()
        {
            _system.ActorSelection("/user/Scheduler/Block1/Block2")
                  .Tell(new SimulateError());
        }

        private static void DisplayActor()
        {
            _system.ActorSelection($"/user/Scheduler/Block1")
                  .Tell(new DisplayStatus());
        }
        private static void DisplayActor2()
        {
            _system.ActorSelection($"/user/Scheduler/Block1/Block2")
                  .Tell(new DisplayStatus());
        }
    }
}
