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
    internal class Block1 : ReceivePersistentActor
    {
        public override string PersistenceId { get; } = "player-block1";
        private RiskJob _riskJob;

        public Block1(RiskJob startingRiskJob)
        {
            _riskJob = startingRiskJob;
            Command<SimulateError>(message => SimulateError());
            Command<ProcessRiskJob>(message => ProcessRiskJob());
            Command<DisplayStatus>(message => DisplayStatus());
            Command<CreateActor>(message => CreateActor(message.ActorName));


            Recover<ActorCreated>(playerCreatedEvent =>
            {
                DisplayHelper.WriteLine($"Scheduler replaying ActorCreated event for Block2");

                Context.ActorOf(
                        Props.Create(() =>
                                    new Block2(_riskJob)), "Block2");

            });
        }

        private void SimulateError()
        {
            DisplayHelper.WriteLine("Block1 received SimulateError");

            throw new ApplicationException($"Simulated exception in player: Block1");
        }

        private void ProcessRiskJob()
        {
            DisplayHelper.WriteLine("Block1 received ProcessRiskJob");

            var @event = new ProcessRiskJob(_riskJob);

            DisplayHelper.WriteLine("Block1 persisting ProcessRiskJob event");

            Persist(@event, riskJobProcessedEvent =>
            {
                DisplayHelper.WriteLine("Block1 persisted riskJobProcessed event ok");
                @event.RiskJob.Name = "RiskJob: Block1 processing";
                @event.RiskJob.SetNextState();
                _riskJob = @event.RiskJob;
            });
        }

        private void DisplayStatus()
        {
            DisplayHelper.WriteLine("Block1 received DisplayStatus");

            Console.WriteLine($"RiskJobName: {_riskJob.Name}, State= {_riskJob.State}");
        }

        private void CreateActor(string actorName)
        {

            DisplayHelper.WriteLine($"Scheduler received CreateActor command for Block1");

            var @event = new ActorCreated(actorName);

            Persist(@event, playerCreatedEvent =>
            {
                DisplayHelper.WriteLine(
                    $"Scheduler persisted a ActorCreated for Block1");

                Context.ActorOf(
                    Props.Create(() =>
                                new Block2(_riskJob)), "Block2");
            });

        }

    }
}
