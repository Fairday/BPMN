using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IMessageSender
    {
        Task SendMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
            where TMessage : class;
        Task SendReturnableMessageAfterDelayAsync<TMessage>(TMessage message, TimeSpan delay, CancellationToken cancellationToken)
            where TMessage : class;
    }
}