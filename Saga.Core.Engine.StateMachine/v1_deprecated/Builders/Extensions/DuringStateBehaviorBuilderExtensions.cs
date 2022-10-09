//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public static class DuringStateBehaviorBuilderExtensions
//    {
//        public static ILeaveStateBehaviorBuilder<TProcessManager> ThenWhenLeave<TProcessManager>(this IDuringStateBehaviorBuilder<TProcessManager> builder)
//            where TProcessManager : IProcessManager<TProcessManager>
//        {
//            var dummyBehavior = new DummyBehavior<TProcessManager>();
//            var state = builder.State;
//            var processManager = builder.ProcessManager;
//            state.BindBehaviorToEvent(state.Leaved, dummyBehavior);
//            return new LeaveStateBehaviorBuilder<TProcessManager>(processManager, state, dummyBehavior);
//        }
//    }
//}