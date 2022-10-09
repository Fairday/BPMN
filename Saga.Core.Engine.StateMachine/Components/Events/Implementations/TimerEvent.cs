using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class TimerEvent : FlowElement, ITimerEvent
    {
        private readonly IMessageSender _messageSender;

        public TimerEvent(string name, IFlowElement flowElement, ConnectionBoundary connectionBoundary, IMessageSender messageSender) : base(name, flowElement, connectionBoundary)
        {
            _messageSender = messageSender;
        }

        public TimerEvent(string name) : base(name)
        {
        }

        public TimeSpan Delay { get; }
        public DateTimeOffset? StartTime { get; }
        public ProcessEventLifetime Lifetime { get; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; }

        public override Task Accept(IProcessVisitor visitor)
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

        public override Task ImpartMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}