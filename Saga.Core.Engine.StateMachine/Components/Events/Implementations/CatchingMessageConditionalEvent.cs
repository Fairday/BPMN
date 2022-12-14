using System;
using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Components.Events
{
    internal sealed class CatchingMessageConditionalEvent<TCatchingMessage> : FlowElement, ICatchingMessageConditionalEvent<TCatchingMessage>
        where TCatchingMessage : class
    {
        public CatchingMessageConditionalEvent(string name, IFlowElement flowElement, ConnectionBoundary connectionBoundary) : base(name, flowElement, connectionBoundary)
        {
        }

        public CatchingMessageConditionalEvent(string name) : base(name)
        {
        }

        public ProcessEventLifetime Lifetime { get; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; }
        public Func<TCatchingMessage, bool> Expression { get; }

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