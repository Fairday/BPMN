//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Threading;
//using System.Threading.Tasks;
//using Saga.Core.Engine.StateMachine.Builders;
//using Saga.Core.Engine.StateMachine.Exceptions;
//using Seedwork.Messaging.Contracts;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public abstract class ProcessManager<TProcessManager> : IProcessManager<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly Queue<object> _outgoingCandidates;
//        private readonly HashSet<IState<TProcessManager>> _states;
//        private readonly HashSet<IProcessEvent> _events;
//        private readonly Dictionary<Type, IProcessEvent> _messageEventPairs;

//        protected ProcessManager()
//        {
//            _outgoingCandidates = new Queue<object>();
//            _states = new HashSet<IState<TProcessManager>>();
//            _events = new HashSet<IProcessEvent>();
//            _messageEventPairs = new Dictionary<Type, IProcessEvent>();
//            ConstructAll();
//        }

//        public IProcessEvent Started { get; }
//        public ITerminateProcessEvent Finished { get; }
//        public ITerminateProcessEvent Aborted { get; }

//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IState<TProcessManager>> InitialStates => _states.Where(s => s.IsInitial);
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IState<TProcessManager> FinalState => _states.Single(s => s.IsFinal);
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IState<TProcessManager>> CurrentStates => States.Where(s => s.IsActive);
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IState<TProcessManager>> States => _states;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        public IEnumerable<IProcessEvent> Events => _events;

//        public async Task Accept(IProcessVisitor visitor)
//        {
//            await visitor.Visit(this);
//            foreach (var state in States)
//                await state.Accept(visitor);
//        }

//        public async Task PropagateEventAsync(IProcessEvent @event, IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            EnsureCanPropagate();
//            if (TryStartProcessStarted(context))
//            {
//                @event.Raise(context);
//                return;
//            }

//            foreach (var currentState in CurrentStates)
//                await currentState.PropagateEventAsync(@event, context, cancellationToken);
//        }

//        public Task PropagateMessageAsync<TMessage>(TMessage message, IEventReactionContext<TMessage, TProcessManager> context,
//            CancellationToken cancellationToken) where TMessage : class
//        {
//            EnsureCanPropagate();

//            if (_messageEventPairs.ContainsKey(message.GetType()))
//            {
//                var @event = (ProcessMessageEvent<TMessage>)_messageEventPairs[message.GetType()];
//                @event.FillMessage(message);
//                TryStartProcessStarted(context);
//                @event.Raise(context);
//            }
//            else
//            {
//                ProcessManagerException.Throw();
//            }

//            return Task.CompletedTask;
//        }

//        public async Task PropagateEventAsync<TMessage>(IProcessEvent @event, IEventReactionContext<TMessage, TProcessManager> context,
//            CancellationToken cancellationToken) where TMessage : class
//        {
//            EnsureCanPropagate();
//            if (TryStartProcessStarted(context))
//            {
//                @event.Raise(context);
//                return;
//            }

//            foreach (var currentState in CurrentStates)
//                await currentState.PropagateEventAsync(@event, context, cancellationToken);
//        }

//        IEnumerable<object> IProcessManager<TProcessManager>.DequeueOutgoingCandidates()
//        {
//            var candidates = new List<object>();
//            while (_outgoingCandidates.Count > 0)
//                candidates.Add(_outgoingCandidates.Dequeue());
//            return candidates;
//        }

//        void IProcessManager<TProcessManager>.EnqueueOutgoingEvent(IEvent @event)
//        {
//            if (@event == null) throw new ArgumentNullException(nameof(@event));
//            _outgoingCandidates.Enqueue(@event);
//        }

//        void IProcessManager<TProcessManager>.EnqueueOutgoingCommand(ICommand command)
//        {
//            if (command == null) throw new ArgumentNullException(nameof(command));
//            _outgoingCandidates.Enqueue(command);
//        }

//        #region rework

//        public void StartProcess(IEventReactionContext<TProcessManager> context)
//        {
//            if (!TryStartProcessStarted(context))
//                ProcessManagerException.Throw();
//        }

//        private bool TryStartProcessStarted(IEventReactionContext context)
//        {
//            if (IsStarted)
//                return false;

//            IsStarted = true;
//            foreach (var currentState in CurrentStates)
//                currentState.Initialize(context);
//            return true;
//        }

//        void IProcessManager<TProcessManager>.AbortProcess(IEventReactionContext<TProcessManager> context)
//        {
//            _outgoingCandidates.Clear();
//            IsAborted = true;
//        }

//        void IProcessManager<TProcessManager>.FinishProcess(IEventReactionContext<TProcessManager> context)
//        {
//            IsFinished = true;
//        }

//        #endregion

//        protected ICompositeEventBuilder<TProcessManager> UnionEvents(params IProcessEvent[] events)
//        {

//        }

//        protected ISingleEventBehaviorBuilder<TProcessManager> Event(IProcessEvent @event)
//        {

//        }

//        protected IStateBehaviorBuilder<TProcessManager> WhenEnter(IState<TProcessManager> state)
//        {
//            var dummyBehavior = new DummyBehavior<TProcessManager>();
//            state.BindBehaviorToEvent(state.Entered, dummyBehavior);
//            var builder = new StateBehaviorBuilder<TProcessManager>(this, state, dummyBehavior);
//            return builder;
//        }

//        protected IDuringStateBehaviorBuilder<TProcessManager> During(IState<TProcessManager> state)
//        {
//            return new DuringStateBehaviorBuilder<TProcessManager>(this, state);
//        }

//        protected ILeaveStateBehaviorBuilder<TProcessManager> WhenLeave(IState<TProcessManager> state)
//        {
//            var dummyBehavior = new DummyBehavior<TProcessManager>();
//            state.BindBehaviorToEvent(state.Leaved, dummyBehavior);
//            return new LeaveStateBehaviorBuilder<TProcessManager>(this, state, dummyBehavior);
//        }

//        private void EnsureCanPropagate()
//        {
//            //think about it
//            if (IsAborted || IsFinished)
//            {
//                ProcessManagerException.Throw();
//            }
//        }

//        //weak place
//        private void ConstructAll()
//        {
//            var processManagerTypeInfo = GetType().GetTypeInfo();

//            var properties = GetProperties(processManagerTypeInfo);

//            foreach (var propertyInfo in properties)
//            {
//                var pTypeInfo = propertyInfo.PropertyType.GetTypeInfo();

//                if (typeof(IProcessEvent).IsAssignableFrom(pTypeInfo))
//                {
//                    var messageType = propertyInfo.PropertyType.GenericTypeArguments[0];
//                    var eventType = typeof(ProcessMessageEvent<>).MakeGenericType(messageType);
//                    var @event = Activator.CreateInstance(eventType, propertyInfo.Name);
//                    InitializeEventProperty(propertyInfo, (dynamic)@event);
//                    _events.Add((IProcessEvent)@event);
//                    _messageEventPairs.Add(messageType, (IProcessEvent)@event);
//                }
//                else if (typeof(IState).IsAssignableFrom(pTypeInfo))
//                {
//                    var stateType = typeof(StateMachineState<>).MakeGenericType(typeof(TProcessManager));
//                    var state = (StateMachineState<TProcessManager>)Activator.CreateInstance(stateType, propertyInfo.Name);
//                    InitializeStateProperty(propertyInfo, state);
//                    _states.Add(state);
//                }
//            }
//        }

//        private IEnumerable<PropertyInfo> GetProperties(TypeInfo typeInfo)
//        {
//            if (typeInfo.IsInterface)
//                yield break;

//            if (typeInfo.BaseType != null)
//            {
//                foreach (var propertyInfo in GetProperties(typeInfo.BaseType.GetTypeInfo()))
//                    yield return propertyInfo;
//            }

//            var properties = typeInfo.DeclaredMethods
//                .Where(x => x.IsSpecialName && x.Name.StartsWith("get_") && !x.IsStatic)
//                .Select(x => typeInfo.GetDeclaredProperty(x.Name["get_".Length..]))
//                // ReSharper disable once PossibleNullReferenceException
//                .Where(x => x.CanRead && x.CanWrite);

//            foreach (var propertyInfo in properties)
//                yield return propertyInfo;
//        }

//        private void InitializeStateProperty(PropertyInfo stateProperty, StateMachineState<TProcessManager> state)
//        {
//            if (stateProperty == null) throw new ArgumentNullException(nameof(stateProperty));
//            if (state == null) throw new ArgumentNullException(nameof(state));
//            if (stateProperty.CanWrite)
//                stateProperty.SetValue(this, state);
//            else
//                throw new ArgumentException($"The state property is not writable: {stateProperty.Name}");
//        }

//        private void InitializeEventProperty<TMessage>(PropertyInfo eventProperty, ProcessMessageEvent<TMessage> messageEvent)
//            where TMessage : class
//        {
//            if (eventProperty.CanWrite)
//                eventProperty.SetValue(this, messageEvent);
//            else
//                throw new ArgumentException($"The event property is not writable: {eventProperty.Name}");
//        }

//        public bool IsOpenGeneric(Type type)
//        {
//            return IsOpenGeneric(type.GetTypeInfo());
//        }

//        public bool IsOpenGeneric(TypeInfo typeInfo)
//        {
//            return typeInfo.IsGenericTypeDefinition || typeInfo.ContainsGenericParameters;
//        }
//    }
//}