using System;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal sealed class TransitionEdge : IInternalEdge<IVertex>
    {
        public TransitionEdge(IConnection connection, IVertex source, IVertex target, bool isProcessAborted)
        {
            IsProduced = connection?.IsProduced ?? throw new ArgumentNullException(nameof(connection));
            IsDisabled = connection?.IsDisabled ?? throw new ArgumentNullException(nameof(connection));
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            IsAborted = !connection.IsProduced && !connection.IsDisabled && isProcessAborted;
        }

        public IVertex Source { get; }
        public IVertex Target { get; }
        public bool IsProduced { get; }
        public bool IsDisabled { get; }
        public bool IsAborted { get; }
    }
}