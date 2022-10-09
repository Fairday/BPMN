using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Exceptions;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    [DebuggerDisplay("Name = {Name}, IsProduced = {IsProduced}, IsDisabled = {IsDisabled}")]
    internal sealed class Connection : FlowElement, IConnection
    {
        private IFlowElement _from;
        private IFlowElement _to;

        internal Connection(IState from, IState to)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
        }

        public Task Accept(IProcessVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public string Name => string.Concat(From.Name, ".", To.Name);
        public bool IsCompleted { get; }
        public bool IsActive { get; }
        public bool IsDisabled { get; internal set; }
        public Task ActivateAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool IsProduced { get; internal set; }
        public IState From { get; }

        IFlowElement IConnection.To => _to;

        IFlowElement IConnection.From => _from;

        public IState To { get; }

        public void Produce(IEventReactionContext context)
        {
            if (IsProduced)
                ProcessManagerException.Throw();

            IsProduced = true;
            To.ReceiveTransition(this, context);
        }

        public void DisableTransition(IEventReactionContext context)
        {
            if (IsDisabled)
                ProcessManagerException.Throw();

            IsDisabled = true;
            To.ReceiveDisabledTransition(this, context);
        }

        public bool Equals(IConnection other)
        {
            return other != null && Name == other.Name;
        }

        public bool Equals(IFlowElement other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is IConnection other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }
}