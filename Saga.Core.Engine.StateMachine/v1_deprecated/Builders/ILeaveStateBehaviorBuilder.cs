//using System;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface ILeaveStateBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionTo(IState<TProcessManager> toState);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIf(IState<TProcessManager> trueToState, StateMachineCondition<TProcessManager> condition);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfNot(IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfElse(IState<TProcessManager> trueToState, IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionTo(IState<TProcessManager> toState, Action<IStateBehaviorBuilder<TProcessManager>> onTransitionBehavior);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIf(IState<TProcessManager> trueToState, StateMachineCondition<TProcessManager> condition, Action<IStateBehaviorBuilder<TProcessManager>> onReceivedBehavior);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfNot(IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition, Action<IStateBehaviorBuilder<TProcessManager>> onReceivedBehavior);
//        ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfElse(IState<TProcessManager> trueToState, IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition, Action<IStateBehaviorBuilder<TProcessManager>> onReceivedBehavior);
//        IActivityBehaviorBuilder<TProcessManager> Finish();
//    }
//}