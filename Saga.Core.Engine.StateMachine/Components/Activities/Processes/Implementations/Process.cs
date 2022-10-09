using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public abstract class Process<TData> : Activity, IProcess<TData>
    {
        private readonly Dictionary<string, IFlowElement> _units;

        protected Process(string name) : base(name)
        {
            Status = ActivityExecutionStatus.NotStarted;

            EventListener.SubscribeToSpecificEventFromSourceAsync<IFinishedEvent>(Activated, async (e, token) =>
            {
                Status = await TryExecuteAsync(token);
                switch (Status)
                {
                    case ActivityExecutionStatus.Faulted:
                    {
                        await ErrorEvent.ActivateAsync(token);
                        break;
                    }
                    case ActivityExecutionStatus.Completed:
                    {
                        await Completed.ActivateAsync(token);
                        break;
                    }
                    case ActivityExecutionStatus.Canceled:
                    {
                        await Canceled.ActivateAsync(token);
                        break;
                    }
                }
            });

            After(Completed)
                .ThenTerminateAt(Finish);

            AfterError(ErrorEvent)
                .ThenTerminateAt(EscalateError);
        }

        public IErrorEvent ErrorEvent { get; }
        public ITerminationEvent Finish { get; }
        public ITerminationEvent EscalateError { get; }
        public IEnumerable<IProcessEvent> AttachedEvents { get; }
        public IEnumerable<IFlowElement> Units => _units.Values;

        public TData Data { get; internal set; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public sealed override async Task ActivateAsync(CancellationToken cancellationToken)
        {
            await Activated.ActivateAsync(cancellationToken);
        }

        public sealed override async Task DisableAsync(CancellationToken cancellationToken)
        {
            await Disabled.ActivateAsync(cancellationToken);
        }

        public async Task ConsumeMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
            where TMessage : class
        {
            foreach (var messageConsumer in Units.OfType<IConsumingMessages>())
                await messageConsumer.ConsumeMessageAsync(message, cancellationToken);
        }

        public void AcceptData(TData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        protected override Task<ActivityUnitOfExecutionResult> OnExecuteAsync(CancellationToken cancellationToken)
            => ActivityUnitOfExecutionResult.Completed().AsTask();

        protected async sealed override Task OnCancelAsync(CancellationToken cancellationToken)
        {
            foreach (var activity in Units.OfType<IActivity>())
                await activity.CancelAsync(cancellationToken);
        }

        private async Task<ActivityExecutionStatus> TryExecuteAsync(CancellationToken cancellationToken)
        {
            if (Status != ActivityExecutionStatus.NotStarted)
                return Status;

            var instantlyCompletedUnits = new HashSet<IFlowElement>();

            foreach (var unit in TopologyTraverser.Traverse(Topology))
            {
                //violence of Demeter's law or not?
                await unit.ActivateAsync(cancellationToken);
                //if (unit.IsCompleted)
                //    instantlyCompletedUnits.Add(processUnit);
            }

            //when to call OnExecuteAsync?
            //does gateway know all about in/out connections? seems like yes
            //topology traverse for gateways?
            //how to disable non-relevant paths?

            //go traverse
            //if all completed => return completed
            //if some in progress => return in progress
            //but what if any nested component escalates error or cancel?
            try
            {
                var activationResultType = await OnExecuteAsync(cancellationToken);
                switch (activationResultType.Status)
                {
                    case ActivityUnitOfExecutionStatus.InProgress:
                    {
                        return ActivityExecutionStatus.Running;
                    }
                    case ActivityUnitOfExecutionStatus.Completed:
                    {
                        return ActivityExecutionStatus.Completed;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(activationResultType), activationResultType, "Unable to handle ActivationResultType {activationResultType}");
                }
            }
            catch (Exception e)
            {
                ErrorEvent.ImpartErrorObject(e);
                return ActivityExecutionStatus.Faulted;
            }
        }
    }
}