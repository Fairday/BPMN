using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class MessageSpreader<TProcessManager> : IMessageSpreader<TProcessManager>
        where TProcessManager : IProcessManager<TProcessManager>
    {
        private readonly TProcessManager _processManager;

        public MessageSpreader(TProcessManager processManager)
        {
            _processManager = processManager;
        }

        public async Task SpreadMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
            where TMessage : class
        {
            var queue = new Queue<IProcessEvent>();
            var context = new EventReactionContext<TMessage, TProcessManager>(_processManager, queue);
            await _processManager.PropagateMessageAsync(message, context, cancellationToken);

            while (queue.Count > 0)
            {
                if (_processManager.IsAborted)
                    return;

                var candidate = queue.Dequeue();
                if (candidate is IProcessEvent refinedCandidate)
                    await _processManager.PropagateEventAsync(refinedCandidate, context, cancellationToken);
                else
                    await _processManager.PropagateEventAsync(candidate, context, cancellationToken);
                candidate.MarkAsPropagated();
            }
        }
    }
}