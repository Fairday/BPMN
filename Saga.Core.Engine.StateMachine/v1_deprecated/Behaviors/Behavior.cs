//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal abstract class Behavior<TProcessManager> : IBehavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private IBehavior<TProcessManager> _nextBehavior;

//        public async Task ExecuteAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            await ExecuteCoreAsync(context, cancellationToken);
//            if (_nextBehavior != null)
//                await _nextBehavior.ExecuteAsync(context, cancellationToken);
//        }

//        public async Task ExecuteAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//            where TMessage : class
//        {
//            await ExecuteCoreAsync(context, cancellationToken);
//            if (_nextBehavior != null)
//                await _nextBehavior.ExecuteAsync(context, cancellationToken);
//        }

//        protected abstract Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken);
//        protected abstract Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//            where TMessage : class;

//        public IBehavior<TProcessManager> ConnectSubsequentBehavior(IBehavior<TProcessManager> behavior)
//        {
//            _nextBehavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
//            return _nextBehavior;
//        }
//    }
//}