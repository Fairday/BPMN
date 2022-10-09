//using System.Collections.Generic;
//using Saga.Core.Engine.StateMachine.Abstractions;

//namespace Saga.Core.Engine.StateMachine.Builders
//{
//    public interface ICompositeEventBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        IEnumerable<IProcessEvent> Events { get; }
//        IEventBehaviorBuilder<TProcessManager> WhenRaisedAny();
//        IEventBehaviorBuilder<TProcessManager> WhenRaisedExactly(params IProcessEvent[] events);
//    }

//    public interface ISingleEventBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        IEventBehaviorBuilder<TProcessManager> WhenRaised();
//    }

//    public interface IEventBehaviorBuilder<TProcessManager> : IActivityBehaviorBuilder<IEventBehaviorBuilder<TProcessManager>, TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        void ThenRaise(IProcessEvent @event);
//    }
//}