//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Seedwork.Messaging.Contracts;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class PublishActivity<TProcessManager> : IActivity<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly Func<IReactionContext<TProcessManager>, IEvent> _eventFactory;

//        public PublishActivity(Func<IReactionContext<TProcessManager>, IEvent> eventFactory)
//        {
//            _eventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
//        }

//        public Task ExecuteAsync(IReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            var @event = _eventFactory(context);
//            context.ProcessManager.EnqueueOutgoingEvent(@event);
//            return Task.CompletedTask;
//        }

//        public void SendTelemetry(ITelemetryContext context)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}