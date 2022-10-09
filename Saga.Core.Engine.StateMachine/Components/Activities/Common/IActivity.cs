using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IActivity
    {
        bool IsCanceled { get; }
        ActivityExecutionStatus Status { get; }
        Task CancelAsync(CancellationToken cancellationToken);
    }

    public interface IActivity<TExecutionOutput> : IActivity
    {
        TExecutionOutput Output { get; }
    }
}