using Akka.Actor;
using Akka.Persistence;
using AkkaNetPersistenceSample.Commands;
using AkkaNetPersistenceSample.Events;
using GameConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample.Actors
{
    internal class Scheduler : ReceivePersistentActor
    {
        private static RiskJob DefaultStartingRiskJob = new RiskJob();
        public override string PersistenceId { get; } = "player-scheduler";

        public Scheduler()
        {
            Command<CreateActor>(cmd =>
            {
                DisplayHelper.WriteLine($"Scheduler received CreateActor command for Block1");

                var @event = new ActorCreated(cmd.ActorName);

                Persist(@event, playerCreatedEvent =>
                {
                    DisplayHelper.WriteLine(
                        $"Scheduler persisted a ActorCreated for Block1");

                    Context.ActorOf(
                        Props.Create(() =>
                                    new Block1(DefaultStartingRiskJob)), "Block1");
                });
            });

            Recover<ActorCreated>(playerCreatedEvent =>
            {
                DisplayHelper.WriteLine($"Scheduler replaying ActorCreated event for Block1");

                Context.ActorOf(
                        Props.Create(() =>
                                    new Block1(DefaultStartingRiskJob)), "Block1");

            });
        }
    }
}
