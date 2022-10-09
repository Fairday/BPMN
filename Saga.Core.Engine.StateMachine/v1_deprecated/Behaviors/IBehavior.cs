//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface IBehavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        Task ExecuteAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken);
//        Task ExecuteAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//            where TMessage : class;
//        IBehavior<TProcessManager> ConnectSubsequentBehavior(IBehavior<TProcessManager> behavior);
//    }
//}