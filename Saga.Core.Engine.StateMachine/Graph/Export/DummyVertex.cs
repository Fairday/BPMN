using System;
using System.Diagnostics;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal sealed class DummyVertex : IVertex
    {
        public enum DummyVertexCategory
        {
            In, Out
        }

        public DummyVertex(string name, DummyVertexCategory category)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Category = category;
        }

        public VertexType Type => VertexType.Dummy;
        public string Name { get; }
        public bool IsAborted => false;
        public DummyVertexCategory Category { get; }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is DummyVertex other && Equals(other);
        }

        [DebuggerStepThrough]
        public bool Equals(DummyVertex other)
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