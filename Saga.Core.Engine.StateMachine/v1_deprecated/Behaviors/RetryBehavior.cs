//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class RetryBehavior<TProcessManager> : Behavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        protected override Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        protected override Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}