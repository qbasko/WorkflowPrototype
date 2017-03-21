using Akka.Persistence;
using AkkaNetPersistenceSample.Commands;
using GameConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetPersistenceSample.Actors
{
    internal class Block2 : ReceivePersistentActor
    {
        public override string PersistenceId { get; } = "player-block2";
        private RiskJob _riskJob;

        public Block2(RiskJob startingRiskJob)
        {
            DisplayHelper.WriteLine("Block2 created");
            _riskJob = startingRiskJob;
            Command<SimulateError>(message => SimulateError());
            Command<DisplayStatus>(message => DisplayStatus());
            Command<ProcessRiskJob>(message => ProcessRiskJob());
        }

        private void DisplayStatus()
        {
            DisplayHelper.WriteLine("Block2 received DisplayStatus");

            Console.WriteLine($"RiskJobName: {_riskJob.Name}, State= {_riskJob.State}");
        }

        private void SimulateError()
        {
            DisplayHelper.WriteLine("Block2 received SimulateError");

            throw new ApplicationException($"Simulated exception in player: Block2");
        }

        private void ProcessRiskJob()
        {
            DisplayHelper.WriteLine("Block2 received ProcessRiskJob");

            var @event = new ProcessRiskJob(_riskJob);

            DisplayHelper.WriteLine("Block2 persisting ProcessRiskJob event");

            Persist(@event, riskJobProcessedEvent =>
            {
                DisplayHelper.WriteLine("Block2 persisted riskJobProcessed event ok");
                @event.RiskJob.Name = "RiskJob: Block2 processing";
                @event.RiskJob.SetNextState();
                _riskJob = @event.RiskJob;
            });
        }
    }
}
