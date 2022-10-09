using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Components.Transitions
{
    internal sealed class ConnectionProxy : IConnection, IIngestSensor
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ISensor _sensor;

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Connection Source { get; }

        [DebuggerStepThrough]
        internal ConnectionProxy(IState from, IState to)
        {
            Source = new Connection(from, to);
        }

        [DebuggerStepThrough]
        public bool Equals(IConnection other)
        {
            return Source.Equals(other);
        }

        [DebuggerStepThrough]
        public Task Accept(IProcessVisitor visitor)
        {
            return Source.Accept(visitor);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Name => Source.Name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled => Source.IsDisabled;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsProduced => Source.IsProduced;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IState From => Source.From;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IState To => Source.To;

        [DebuggerStepThrough]
        public void Produce(IEventReactionContext context)
        {
            Source.Produce(context);
        }

        [DebuggerStepThrough]
        public void DisableTransition(IEventReactionContext context)
        {
            Source.DisableTransition(context);
        }

        public void IngestSensor(ISensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
        }
    }
}