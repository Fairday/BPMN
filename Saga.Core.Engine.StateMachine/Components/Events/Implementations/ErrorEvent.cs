using System;
using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Helpers;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ErrorEvent : FlowElement, IErrorEvent

    {
        public ErrorEvent(string name, IFlowElement flowElement, ConnectionBoundary connectionBoundary) : base(name, flowElement, connectionBoundary)
        {
        }

        public ErrorEvent(string name) : base(name)
        {
        }

        public ProcessEventLifetime Lifetime { get; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; }
        public IActivity BoundedActivity { get; }
        public Exception Error { get; }

        public void ImpartErrorObject(Exception error)
        {
            throw new NotImplementedException();
        }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override Task ImpartMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task ActivateAsync(CancellationToken cancellationToken)
        {
            Output.AddDataOrThrow(Error);
            Output.TryActivateAsync(cancellationToken);

            throw new NotImplementedException();
        }

        public override Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task CompleteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}