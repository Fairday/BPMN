//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class TransitionToIfNotBehavior<TProcessManager> : Behavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IState<TProcessManager> _fromState;
//        private readonly IState<TProcessManager> _falseToState;
//        private readonly StateMachineCondition<TProcessManager> _condition;

//        public TransitionToIfNotBehavior(IState<TProcessManager> fromState, IState<TProcessManager> falseToState, StateMachineCondition<TProcessManager> condition)
//        {
//            _fromState = fromState ?? throw new ArgumentNullException(nameof(fromState));
//            _falseToState = falseToState ?? throw new ArgumentNullException(nameof(falseToState));
//            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
//            _fromState.AddTransitionTo(_falseToState);
//        }

//        protected override Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            if (_condition(context))
//                _fromState.DisableTransitionTo(_falseToState, context);
//            return Task.CompletedTask;
//        }

//        protected override Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//        {
//            if (_condition(context))
//                _fromState.DisableTransitionTo(_falseToState, context);
//            return Task.CompletedTask;
//        }
//    }
//}