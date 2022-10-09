using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ThrowingMessageEvent<TThrowingMessage> : FlowElement, IThrowingMessageEvent<TThrowingMessage>
        where  TThrowingMessage : class
    {
        public ThrowingMessageEvent(string name, IFlowElement flowElement, ConnectionBoundary connectionBoundary) : base(name, flowElement, connectionBoundary)
        {
        }

        public ThrowingMessageEvent(string name) : base(name)
        {
        }

        public ProcessEventLifetime Lifetime { get; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; }
        public Func<TThrowingMessage> ThrowingMessageFactory { get; }

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
            throw new NotImplementedException();
        }

        public override Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}