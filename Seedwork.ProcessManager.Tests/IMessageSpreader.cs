using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IMessageSpreader<TProcessManager>
        where TProcessManager : IProcessManager<TProcessManager>
    {
        Task SpreadMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
            where TMessage : class;
    }
}