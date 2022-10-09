//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class TransitionToIfElseBehavior<TProcessManager> : Behavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IState<TProcessManager> _fromState;
//        private readonly IState<TProcessManager> _trueToState;
//        private readonly IState<TProcessManager> _falseToState;
//        private readonly StateMachineCondition<TProcessManager> _condition;

//        public TransitionToIfElseBehavior(IState<TProcessManager> fromState, IState<TProcessManager> trueToState, IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition)
//        {
//            _fromState = fromState ?? throw new ArgumentNullException(nameof(fromState));
//            _trueToState = trueToState ?? throw new ArgumentNullException(nameof(trueToState));
//            _falseToState = falseToState ?? throw new ArgumentNullException(nameof(falseToState));
//            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
//            _fromState.AddTransitionTo(_trueToState);
//            _fromState.AddTransitionTo(_falseToState);
//        }

//        protected override Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            var conditionResult = _condition(context);
//            _fromState.DisableTransitionTo(conditionResult ? _falseToState : _trueToState, context);
//            _fromState.ProduceTransitionTo(conditionResult ? _trueToState : _falseToState, context);
//            return Task.CompletedTask;
//        }

//        protected override Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//        {
//            var conditionResult = _condition(context);
//            _fromState.DisableTransitionTo(conditionResult ? _falseToState : _trueToState, context);
//            _fromState.ProduceTransitionTo(conditionResult ? _trueToState : _falseToState, context);
//            return Task.CompletedTask;
//        }
//    }
//}