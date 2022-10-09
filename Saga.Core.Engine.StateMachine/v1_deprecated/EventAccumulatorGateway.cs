//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Saga.Core.Engine.StateMachine.Exceptions;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface IEventAccumulatorGateway : IProcessGateway
//    {
//        IProcessEvent Entered { get; }
//        IProcessEvent Leaved { get; }
//        IEnumerable<IProcessEvent> LeavePrerequisites { get; }
//        bool IsInitial { get; }
//        bool IsFinal { get; }
//        bool IsActive { get; }
//        bool IsDisabled { get; }
//        void AddTransitionTo(IState to);
//        void AddEnterPrerequisite(IConnection prerequisite);
//        void AddLeavePrerequisite(IProcessEvent prerequisite);
//        void ProduceTransitionTo(IState to, IReactionContext context);
//        void ReceiveTransition(IConnection connection, IReactionContext context);
//        void DisableTransitionTo(IState to, IReactionContext context);
//        void ReceiveDisabledTransition(IConnection disabledConnection, IReactionContext context);
//        void Initialize(IReactionContext context);
//        bool IsPrerequisiteResolved(IConnection prerequisite);
//        bool IsPrerequisiteResolved(IProcessEvent prerequisite);
//        IProcessEvent GetLeavePrerequisite(string name);
//    }

//    public interface IEventAccumulatorGateway<TProcessManager> : IEventAccumulatorGateway
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        void BindToSuperState(IState<TProcessManager> superState);
//        void BindBehaviorToEvent(IProcessEvent @event, IBehavior<TProcessManager> behavior);
//        IState<TProcessManager> SuperState { get; }
//        Task PropagateEventAsync(IProcessEvent @event, IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken);
//        Task PropagateEventAsync<TMessage>(IProcessEvent @event, IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//            where TMessage : class;
//    }

//    [DebuggerDisplay("Name = {Name}, IsInitial = {IsInitial}, IsFinal = {IsFinal}, IsActive = {IsActive}, IsDisabled = {IsDisabled}")]
//    internal sealed class EventAccumulatorGateway<TProcessManager> : IEventAccumulatorGateway<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IDictionary<IProcessEvent, IBehavior<TProcessManager>> _stateBehaviors;
//        // ReSharper disable once InconsistentNaming
//        internal readonly Dictionary<IConnection, bool> _enterPrerequisites;
//        // ReSharper disable once InconsistentNaming
//        internal readonly Dictionary<IProcessEvent, bool> _leavePrerequisites;
//        // ReSharper disable once InconsistentNaming
//        internal readonly Dictionary<string, IProcessEvent> _eventsMap;
//        // ReSharper disable once InconsistentNaming
//        internal readonly Dictionary<IConnection, bool> _leaveTransitions;

//        public StateMachineState(string name)
//        {
//            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
//            Name = name;

//            _stateBehaviors = new Dictionary<IProcessEvent, IBehavior<TProcessManager>>();
//            _enterPrerequisites = new Dictionary<IConnection, bool>();
//            _leavePrerequisites = new Dictionary<IProcessEvent, bool>();
//            _eventsMap = new Dictionary<string, IProcessEvent>();
//            _leaveTransitions = new Dictionary<IConnection, bool>();

//            Entered = new PlainProcessEvent(Name + "." + nameof(Entered));
//            Leaved = new PlainProcessEvent(Name + "." + nameof(Leaved));

//            AddLeavePrerequisite(Entered);
//        }

//        public string Name { get; }
//        public IProcessEvent Entered { get; }
//        public IProcessEvent Leaved { get; }
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IConnection> EnterPrerequisites => _enterPrerequisites.Keys;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IProcessEvent> LeavePrerequisites => _leavePrerequisites.Keys;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IConnection> LeaveTransitions => _leaveTransitions.Keys;
//        public IState<TProcessManager> SuperState { get; private set; }
//        public bool IsInitial => _enterPrerequisites.Count == 0;
//        public bool IsFinal => _leaveTransitions.Count == 0;
//        public bool IsActive => !IsDisabled && _enterPrerequisites.All(p => p.Value) && !Leaved.IsPropagated;
//        public bool IsDisabled { get; internal set; }

//        public async Task Accept(IProcessVisitor visitor)
//        {
//            await visitor.Visit(this);

//            foreach (var leavePrerequisite in LeavePrerequisites)
//                await leavePrerequisite.Accept(visitor);

//            await Leaved.Accept(visitor);

//            foreach (var leaveTransition in LeaveTransitions)
//                await leaveTransition.Accept(visitor);
//        }

//        public async Task PropagateEventAsync(IProcessEvent @event, IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            if (IsDisabled)
//                return;

//            if (!EnsureIsMyPrerequisite(@event))
//                return;

//            if (!Entered.IsRaised)
//            {
//                Entered.Raise(context);
//                @event.Raise(context);
//                return;
//            }

//            if (Equals(@event, Leaved))
//            {
//                if (!_leavePrerequisites.All(p => p.Value))
//                    ProcessManagerException.Throw();

//                if (_stateBehaviors.ContainsKey(@event))
//                    await _stateBehaviors[@event].ExecuteAsync(context, cancellationToken);
//            }
//            else
//            {
//                if (IsPrerequisiteResolved(@event))
//                    return;

//                if (_stateBehaviors.ContainsKey(@event))
//                    await _stateBehaviors[@event].ExecuteAsync(context, cancellationToken);
//                ResolvePrerequisite(@event);

//                if (_leavePrerequisites.All(p => p.Value))
//                    Leaved.Raise(context);
//            }
//        }

//        public async Task PropagateEventAsync<TMessage>(IProcessEvent @event, IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//            where TMessage : class
//        {
//            if (IsDisabled)
//                return;

//            if (!EnsureIsMyPrerequisite(@event))
//                return;

//            if (!Entered.IsRaised)
//            {
//                Entered.Raise(context);
//                @event.Raise(context);
//                return;
//            }

//            if (Equals(@event, Leaved))
//            {
//                if (!_leavePrerequisites.All(p => p.Value))
//                    ProcessManagerException.Throw();

//                if (_stateBehaviors.ContainsKey(@event))
//                    await _stateBehaviors[@event].ExecuteAsync(context, cancellationToken);
//            }
//            else
//            {
//                if (IsPrerequisiteResolved(@event))
//                    return;

//                if (_stateBehaviors.ContainsKey(@event))
//                    await _stateBehaviors[@event].ExecuteAsync(context, cancellationToken);
//                ResolvePrerequisite(@event);

//                if (_leavePrerequisites.All(p => p.Value))
//                    Leaved.Raise(context);
//            }
//        }

//        public void BindToSuperState(IState<TProcessManager> superState)
//        {
//            if (SuperState != null)
//                ProcessManagerException.Throw();

//            SuperState = superState ?? throw new ArgumentNullException(nameof(superState));
//        }

//        public void BindBehaviorToEvent(IProcessEvent @event, IBehavior<TProcessManager> behavior)
//        {
//            if (@event == null) throw new ArgumentNullException(nameof(@event));

//            if (_stateBehaviors.ContainsKey(@event))
//                ProcessManagerException.Throw();

//            _stateBehaviors[@event] = behavior ?? throw new ArgumentNullException(nameof(behavior));
//        }

//        public void AddTransitionTo(IState to)
//        {
//            var transition = ConnectionFactory.Create(this, to);
//            if (_leaveTransitions.ContainsKey(transition))
//                ProcessManagerException.Throw();
//            _leaveTransitions.Add(transition, false);
//            to.AddEnterPrerequisite(transition);
//        }

//        public void AddEnterPrerequisite(IConnection prerequisite)
//        {
//            if (EnsureIsMyPrerequisite(prerequisite))
//                ProcessManagerException.Throw();
//            _enterPrerequisites.Add(prerequisite, false);
//        }

//        public void AddLeavePrerequisite(IProcessEvent prerequisite)
//        {
//            if (EnsureIsMyPrerequisite(prerequisite))
//                ProcessManagerException.Throw();
//            _leavePrerequisites.Add(prerequisite, false);
//            _eventsMap.Add(prerequisite.Name, prerequisite);
//        }

//        public void ProduceTransitionTo(IState to, IEventReactionContext context)
//        {
//            var transition = LeaveTransitions.Single(t => Equals(t.To, to));

//            if (_leaveTransitions[transition])
//                ProcessManagerException.Throw();

//            transition.Produce(context);
//            ResolveTransition(transition);
//        }

//        public void ReceiveTransition(IConnection connection, IEventReactionContext context)
//        {
//            if (IsPrerequisiteResolved(connection))
//                ProcessManagerException.Throw();

//            ResolvePrerequisite(connection);
//            TryInitialize(context);
//        }

//        public void DisableTransitionTo(IState to, IEventReactionContext context)
//        {
//            var transition = LeaveTransitions.Single(t => Equals(t.To, to));

//            if (_leaveTransitions[transition])
//                ProcessManagerException.Throw();

//            transition.DisableTransition(context);
//            ResolveTransition(transition);
//        }

//        public void ReceiveDisabledTransition(IConnection disabledConnection, IEventReactionContext context)
//        {
//            if (IsPrerequisiteResolved(disabledConnection))
//                ProcessManagerException.Throw();

//            ResolvePrerequisite(disabledConnection);

//            if (EnterPrerequisites.All(p => p.IsDisabled))
//            {
//                IsDisabled = true;
//                foreach (var transition in LeaveTransitions)
//                    transition.DisableTransition(context);
//            }
//            else
//                TryInitialize(context);
//        }

//        public void Initialize(IEventReactionContext context)
//        {
//            if (!TryInitialize(context))
//                ProcessManagerException.Throw();
//        }

//        public bool IsPrerequisiteResolved(IConnection prerequisite)
//        {
//            EnsureIsMyPrerequisiteOrThrow(prerequisite);
//            return _enterPrerequisites[prerequisite];
//        }

//        public bool IsPrerequisiteResolved(IProcessEvent prerequisite)
//        {
//            EnsureIsMyPrerequisiteOrThrow(prerequisite);
//            return _leavePrerequisites[prerequisite];
//        }

//        public IProcessEvent GetLeavePrerequisite(string name)
//        {
//            return _eventsMap[name];
//        }

//        public bool Equals(IState other)
//        {
//            return other != null && Name == other.Name;
//        }

//        public int CompareTo(IState other)
//        {
//            return string.CompareOrdinal(Name, other.Name);
//        }

//        public override bool Equals(object obj)
//        {
//            if (ReferenceEquals(null, obj))
//                return false;
//            if (ReferenceEquals(this, obj))
//                return true;
//            return obj is IState other && Equals(other);
//        }

//        internal void ResolvePrerequisite(IProcessEvent prerequisite)
//        {
//            if (prerequisite == null) 
//                throw new ArgumentNullException(nameof(prerequisite));

//            _leavePrerequisites[prerequisite] = true;
//        }

//        internal void ResolvePrerequisite(IConnection prerequisite)
//        {
//            if (prerequisite == null) 
//                throw new ArgumentNullException(nameof(prerequisite));

//            _enterPrerequisites[prerequisite] = true;
//        }

//        internal void ResolveTransition(IConnection connection)
//        {
//            if (connection == null)
//                throw new ArgumentNullException(nameof(connection));
//            _leaveTransitions[connection] = true;
//        }

//        public override int GetHashCode()
//        {
//            return Name?.GetHashCode() ?? 0;
//        }

//        private void EnsureIsMyPrerequisiteOrThrow(IProcessEvent prerequisite)
//        {
//            if (!EnsureIsMyPrerequisite(prerequisite))
//                ProcessManagerException.Throw();
//        }

//        private bool EnsureIsMyPrerequisite(IProcessEvent prerequisite)
//        {
//            if (prerequisite == null)
//                throw new ArgumentNullException(nameof(prerequisite));

//            return _leavePrerequisites.ContainsKey(prerequisite) || Equals(prerequisite, Leaved);
//        }

//        private void EnsureIsMyPrerequisiteOrThrow(IConnection prerequisite)
//        {
//            if (!EnsureIsMyPrerequisite(prerequisite))
//                ProcessManagerException.Throw();
//        }

//        private bool EnsureIsMyPrerequisite(IConnection prerequisite)
//        {
//            if (prerequisite == null)
//                throw new ArgumentNullException(nameof(prerequisite));

//            return _enterPrerequisites.ContainsKey(prerequisite);
//        }

//        private bool TryInitialize(IEventReactionContext context)
//        {
//            if (!_enterPrerequisites.All(kvp => kvp.Value) || Entered.IsRaised)
//                return false;

//            Entered.Raise(context);
//            var eventsWithMessages = _leavePrerequisites.Keys.OfType<IProcessEvent>().ToList();
//            foreach (var eventWithMessage in eventsWithMessages.Where(eventWithMessage => eventWithMessage.Message != null && !IsPrerequisiteResolved(eventWithMessage)))
//                eventWithMessage.RaiseAgain(context);
//            return true;
//        }
//    }
//}