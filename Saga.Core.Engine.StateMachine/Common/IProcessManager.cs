using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seedwork.Messaging.Contracts;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessManager<TProcessManager> : IVisitable
        where TProcessManager : IProcessManager<TProcessManager>
    {
        IEnumerable<object> DequeueOutgoingCandidates();
        void EnqueueOutgoingEvent(IEvent @event);
        void EnqueueOutgoingCommand(ICommand command);
        void StartProcess();
        void AbortProcess();
        void FinishProcess();
        Task PropagateMessageAsync<TMessage>(TMessage message, IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
            where TMessage : class;
    }
}