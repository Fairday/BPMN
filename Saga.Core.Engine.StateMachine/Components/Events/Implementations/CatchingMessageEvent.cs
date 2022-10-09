using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Components.Events.Implementations
{
    internal sealed class CatchingMessageEvent<TCatchingMessage> : FlowElement, ICatchingMessageEvent<TCatchingMessage>
        where TCatchingMessage : class
    {
        public CatchingMessageEvent(string name) : base(name)
        {
        }

        public ProcessEventLifetime Lifetime { get; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; }
        public TCatchingMessage CaughtMessage { get; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public override Task ConsumeMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override Task ActivateAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}