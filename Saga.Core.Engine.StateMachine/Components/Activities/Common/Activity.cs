using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public abstract class Activity<TExecutionOutput> : Activity, IActivity<TExecutionOutput>
    {
        protected Activity(string name) : base(name)
        {
        }

        public TExecutionOutput Output { get; internal set; }
    }

    public abstract class Activity : FlowObject, IActivity
    {
        protected Activity(string name) : base(name)
        {
            EventListener.SubscribeToSpecificEventFromSourceAsync<IFinishedEvent>(Canceled, async (e, token) =>
            {
                //what if cancellation is requiring more time?
                await OnCancelAsync(token);
                Status = ActivityExecutionStatus.Canceled;
                IsCanceled = true;
            });
        }

        public bool IsCanceled { get; internal set; }
        public ActivityExecutionStatus Status { get; internal set; }
        protected IEmptyEvent Canceled { get; }

        public async Task CancelAsync(CancellationToken cancellationToken)
        {
            await Canceled.ActivateAsync(cancellationToken);
        }

        protected abstract Task<ActivityUnitOfExecutionResult> OnExecuteAsync(CancellationToken cancellationToken);
        protected abstract Task OnCancelAsync(CancellationToken cancellationToken);
    }
}