//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class TransitionToBehavior<TProcessManager> : Behavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IState<TProcessManager> _fromState;
//        private readonly IState<TProcessManager> _toState;

//        public TransitionToBehavior(IState<TProcessManager> fromState, IState<TProcessManager> toState)
//        {
//            _fromState = fromState ?? throw new ArgumentNullException(nameof(fromState));
//            _toState = toState ?? throw new ArgumentNullException(nameof(toState));
//            fromState.AddTransitionTo(toState);
//        }

//        protected override Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            _fromState.ProduceTransitionTo(_toState, context);
//            return Task.CompletedTask;
//        }

//        protected override Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//        {
//            _fromState.ProduceTransitionTo(_toState, context);
//            return Task.CompletedTask;
//        }
//    }
//}