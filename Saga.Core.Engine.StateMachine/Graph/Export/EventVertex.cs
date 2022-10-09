using System;
using System.Diagnostics;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal sealed class EventVertex : IVertex
    {
        public EventVertex(IProcessEvent @event, bool isDisabled, bool isProcessAborted)
        {
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
            IsDisabled = isDisabled;
            IsAborted = !@event.IsPropagated && isProcessAborted;
        }

        public IProcessEvent Event { get; }
        public bool IsDisabled { get; }
        public VertexType Type => VertexType.Event;
        public string Name => Event.Name;
        public bool IsAborted { get; }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is EventVertex other && Equals(other);
        }

        [DebuggerStepThrough]
        public bool Equals(EventVertex other)
        {
            return other != null && Name == other.Name;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }
}