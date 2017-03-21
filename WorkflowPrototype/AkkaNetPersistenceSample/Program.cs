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
                    CreateBlock1(actorName);
                }
                else if (action.Contains("display1"))
                {
                    DisplayActor(actorName);
                }
                else if (action.Contains("display2"))
                {
                    DisplayActor2(actorName);
                }
                else if (action.Contains("error1"))
                {
                    ErrorPlayer(actorName);
                }
                else if (action.Contains("process1"))
                {
                    ProcessRiskJob(actorName);
                }
                else if (action.Contains("create2"))
                {
                    CreateBlock2(actorName);
                }
                else if (action.Contains("error2"))
                {
                    ErrorPlayer2(actorName);
                }
                else if (action.Contains("process2"))
                {
                    ProcessRiskJob2(actorName);
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
            WriteLine("<playername> create1");
            WriteLine("<playername> display1");
            WriteLine("<playername> error1");
            WriteLine("<playername> process1");
        }

        private static void CreateBlock1(string actorName)
        {
            _scheduler.Tell(new CreateActor(actorName));
        }

        private static void CreateBlock2(string actorName)
        {
            var actor = _system.ActorSelection("/user/Scheduler/Block1");
            actor.Tell(new CreateActor(actorName));
        }

        private static void ProcessRiskJob(string actorName)
        {
            _system.ActorSelection($"/user/Scheduler/{actorName}")
                  .Tell(new ProcessRiskJob());
        }

        private static void ProcessRiskJob2(string actorName)
        {
            _system.ActorSelection($"/user/Scheduler/Block1/{actorName}")
                  .Tell(new ProcessRiskJob());
        }

        private static void ErrorPlayer(string actorName)
        {
            _system.ActorSelection($"/user/Scheduler/{actorName}")
                  .Tell(new SimulateError());
        }

        private static void ErrorPlayer2(string actorName)
        {
            _system.ActorSelection($"/user/Scheduler/Block1/{actorName}")
                  .Tell(new SimulateError());
        }

        private static void DisplayActor(string actorName)
        {
            _system.ActorSelection($"/user/Scheduler/{actorName}")
                  .Tell(new DisplayStatus());
        }
        private static void DisplayActor2(string actorName)
        {
            _system.ActorSelection($"/user/Scheduler/Block1/{actorName}")
                  .Tell(new DisplayStatus());
        }
    }
}
