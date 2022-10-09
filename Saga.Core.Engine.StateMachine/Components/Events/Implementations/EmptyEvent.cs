using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Components.Events.Implementations
{
    internal sealed class EmptyEvent : ProcessEvent, IEmptyEvent
    {
        public EmptyEvent(string name) : base(name)
        {
            BoundaryBehavior = ActivityBoundaryBehavior.NonInterrupting;
        }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public override async Task ActivateAsync(CancellationToken cancellationToken)
        {
            await Activated.ActivateAsync(cancellationToken);
            await Completed.ActivateAsync(cancellationToken);
        }

        public override async Task DisableAsync(CancellationToken cancellationToken)
        {
            await Disabled.ActivateAsync(cancellationToken);
        }
    }
}