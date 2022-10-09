//using System;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class LeaveStateBehaviorBuilder<TProcessManager> : ILeaveStateBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IProcessManager<TProcessManager> _processManager;
//        private readonly IState<TProcessManager> _leavedState;
//        private IBehavior<TProcessManager> _lastBehavior;

//        public LeaveStateBehaviorBuilder(IProcessManager<TProcessManager> processManager, IState<TProcessManager> leavedState, IBehavior<TProcessManager> behavior)
//        {
//            _processManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
//            _leavedState = leavedState ?? throw new ArgumentNullException(nameof(leavedState));
//            _lastBehavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionTo(IState<TProcessManager> toState)
//        {
//            var transitionBehavior = new TransitionToBehavior<TProcessManager>(_leavedState, toState);
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(transitionBehavior);
//            return this;
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIf(IState<TProcessManager> trueToState, StateMachineCondition<TProcessManager> condition)
//        {
//            var transitionBehavior = new TransitionToIfBehavior<TProcessManager>(_leavedState, trueToState, condition);
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(transitionBehavior);
//            return this;
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfNot(IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition)
//        {
//            var transitionBehavior = new TransitionToIfNotBehavior<TProcessManager>(_leavedState, falseToState, condition);
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(transitionBehavior);
//            return this;
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfElse(IState<TProcessManager> trueToState, IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition)
//        {
//            var transitionBehavior = new TransitionToIfElseBehavior<TProcessManager>(_leavedState, trueToState, falseToState, condition);
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(transitionBehavior);
//            return this;
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionTo(IState<TProcessManager> toState, Action<IStateBehaviorBuilder<TProcessManager>> onTransitionBehavior)
//        {
//            throw new NotImplementedException();
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIf(IState<TProcessManager> trueToState, StateMachineCondition<TProcessManager> condition,
//            Action<IStateBehaviorBuilder<TProcessManager>> onReceivedBehavior)
//        {
//            throw new NotImplementedException();
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfNot(IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition,
//            Action<IStateBehaviorBuilder<TProcessManager>> onReceivedBehavior)
//        {
//            throw new NotImplementedException();
//        }

//        public ILeaveStateBehaviorBuilder<TProcessManager> TransitionToIfElse(IState<TProcessManager> trueToState, IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition,
//            Action<IStateBehaviorBuilder<TProcessManager>> onReceivedBehavior)
//        {
//            throw new NotImplementedException();
//        }

//        public IActivityBehaviorBuilder<TProcessManager> Finish()
//        {
//            var finishProcessBehavior = new FinishProcessBehavior<TProcessManager>();
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(finishProcessBehavior);
//            var builder = new ActivityBehaviorBuilder<TProcessManager>(_processManager, _leavedState, finishProcessBehavior);
//            return builder;
//        }
//    }
//}