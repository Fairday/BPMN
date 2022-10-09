//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class TransitionToIfBehavior<TProcessManager> : Behavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IState<TProcessManager> _fromState;
//        private readonly IState<TProcessManager> _trueToState;
//        private readonly StateMachineCondition<TProcessManager> _condition;

//        public TransitionToIfBehavior(IState<TProcessManager> fromState, IState<TProcessManager> trueToState, StateMachineCondition<TProcessManager> condition)
//        {
//            _fromState = fromState ?? throw new ArgumentNullException(nameof(fromState));
//            _trueToState = trueToState ?? throw new ArgumentNullException(nameof(trueToState));
//            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
//            _fromState.AddTransitionTo(_trueToState);
//        }

//        protected override Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            if (!_condition(context))
//                _fromState.DisableTransitionTo(_trueToState, context);
//            return Task.CompletedTask;
//        }

//        protected override Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//        {
//            if (!_condition(context))
//                _fromState.DisableTransitionTo(_trueToState, context);
//            return Task.CompletedTask;
//        }
//    }
//}