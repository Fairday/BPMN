//using System.Linq;
//using Saga.Core.Engine.StateMachine.Abstractions;

//namespace Saga.Core.Engine.StateMachine.Builders
//{
//    public static class CompositeEventBuilderExtensions
//    {
//        public static IEventBehaviorBuilder<TProcessManager> WhenRaisedAll<TProcessManager>(this ICompositeEventBuilder<TProcessManager> builder)
//            where TProcessManager : IProcessManager<TProcessManager>
//        {
//            return builder.WhenRaisedExactly(builder.Events.ToArray());
//        }

//        public static IEventBehaviorBuilder<TProcessManager> WhenRaisedFirst<TProcessManager>(this ICompositeEventBuilder<TProcessManager> builder)
//            where TProcessManager : IProcessManager<TProcessManager>
//        {
//            return builder.WhenRaisedExactly(builder.Events.First());
//        }

//        public static IEventBehaviorBuilder<TProcessManager> WhenRaisedLast<TProcessManager>(this ICompositeEventBuilder<TProcessManager> builder)
//            where TProcessManager : IProcessManager<TProcessManager>
//        {
//            return builder.WhenRaisedExactly(builder.Events.Last());
//        }
//    }
//}