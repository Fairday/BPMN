using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessEventRiser
    {
        Task RaiseAsync<TInternalEvent>(TInternalEvent internalEvent, CancellationToken cancellationToken)
            where TInternalEvent : IInternalEvent;
    }
}