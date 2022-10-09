//using System;
//using System.Threading.Tasks;
//using Seedwork.Messaging.Contracts;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface IActivityBehaviorBuilder<out TBuilder, TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//        where TBuilder : class
//    {
//        TBuilder Then(Action<IReactionContext<TProcessManager>> action, Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null);
//        TBuilder ThenAsync(Func<IReactionContext<TProcessManager>, Task> actionAsync, Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null);
//        TBuilder Publish(Func<IReactionContext<TProcessManager>, IEvent> eventFactory);
//        TBuilder Send(Func<IReactionContext<TProcessManager>, ICommand> commandFactory);
//        TBuilder Custom<TActivity>(Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null)
//            where TActivity : IActivity<TProcessManager>;
//    }

//    public interface IActivityBehaviorBuilder<TProcessManager> : IActivityBehaviorBuilder<IActivityBehaviorBuilder<TProcessManager>, TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//    }
//}