using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ICompensationActivity : IActivity
    {
        Task CompensateAsync(CancellationToken cancellationToken);
    }
}