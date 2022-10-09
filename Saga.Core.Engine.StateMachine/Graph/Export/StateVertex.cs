using System;
using System.Diagnostics;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal sealed class StateVertex : IVertex
    {
        public StateVertex(IState state, bool processStarted, bool isProcessAborted)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
            ProcessStarted = processStarted;
            IsAborted = !state.Leaved.IsPropagated && isProcessAborted;
        }

        public IState State { get; }
        public bool ProcessStarted { get; }
        public VertexType Type => VertexType.State;
        public string Name => State.Name;
        public bool IsAborted { get; }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is StateVertex other && Equals(other);
        }

        [DebuggerStepThrough]
        public bool Equals(StateVertex other)
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