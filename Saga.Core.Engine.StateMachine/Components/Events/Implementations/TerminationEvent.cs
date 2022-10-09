using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class TerminationEvent : FlowElement, ITerminationEvent
    {
        public TerminationEvent(string name, IFlowElement flowElement, ConnectionBoundary connectionBoundary) : base(name, flowElement, connectionBoundary)
        {
        }

        public TerminationEvent(string name) : base(name)
        {
        }

        public ProcessEventLifetime Lifetime { get; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; }
        public TerminationBehavior TerminationBehavior { get; }
        public IProcess AttachedToProcess { get; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public override Task ImpartMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override Task ActivateAsync(CancellationToken cancellationToken)
        {
            if (TerminationBehavior == TerminationBehavior.Cancel)
                AttachedProcess.CancelAsync();

            throw new System.NotImplementedException();
        }

        public override Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override Task CompleteAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}