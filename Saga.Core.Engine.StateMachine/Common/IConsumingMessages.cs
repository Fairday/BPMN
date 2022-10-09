using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IConsumingMessages
    {
        Task ConsumeMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
            where TMessage : class;
    }
}