using System;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal sealed class GeneralEdge : IInternalEdge<IVertex>
    {
        public GeneralEdge(IVertex source, IVertex target, bool isProduced, bool isDisabled, bool isAborted)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            IsProduced = isProduced;
            IsDisabled = isDisabled;
            IsAborted = isAborted;
        }

        public IVertex Source { get; }
        public IVertex Target { get; }
        public bool IsProduced { get; }
        public bool IsDisabled { get; }
        public bool IsAborted { get; }
    }
}