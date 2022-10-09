//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface IStateBehaviorBuilder<out TBuilder, TProcessManager> : IActivityBehaviorBuilder<TBuilder, TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//        where TBuilder : class
//    {
//        void DoNothing();
//        void AbortProcess();
//        TBuilder DoNothingIf(StateMachineCondition<TProcessManager> condition);
//        TBuilder AbortProcessIf(StateMachineCondition<TProcessManager> condition);
//    }

//    public interface IStateBehaviorBuilder<TProcessManager> : IStateBehaviorBuilder<IStateBehaviorBuilder<TProcessManager>, TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        IDuringStateBehaviorBuilder<TProcessManager> ThenDuring();
//    }
//}