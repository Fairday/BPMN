//using System;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class DuringStateBehaviorBuilder<TProcessManager> : IDuringStateBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        public DuringStateBehaviorBuilder(IProcessManager<TProcessManager> processManager, IState<TProcessManager> state)
//        {
//            ProcessManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
//            State = state ?? throw new ArgumentNullException(nameof(state));
//        }

//        public IProcessManager<TProcessManager> ProcessManager { get; }
//        public IState<TProcessManager> State { get; }

//        public IDuringStateBehaviorBuilder<TProcessManager> WhenReceived(IProcessEvent @event, Action<IStateBehaviorBuilder<TProcessManager>> onReceiveBehavior) 
//        {
//            var dummyBehavior = new DummyBehavior<TProcessManager>();
//            var builder = new StateBehaviorBuilder<TProcessManager>(ProcessManager, State, dummyBehavior);
//            onReceiveBehavior(builder);
//            State.AddLeavePrerequisite(@event);
//            State.BindBehaviorToEvent(@event, dummyBehavior);
//            return this;
//        }

//        public IDuringStateBehaviorBuilder<TProcessManager> JustWait(IProcessEvent @event)
//        {
//            State.AddLeavePrerequisite(@event);
//            return this;
//        }
//    }
//}