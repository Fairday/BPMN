//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Seedwork.Messaging.Contracts;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class SendActivity<TProcessManager> : IActivity<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly Func<IReactionContext<TProcessManager>, ICommand> _commandFactory;

//        public SendActivity(Func<IReactionContext<TProcessManager>, ICommand> commandFactory)
//        {
//            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
//        }

//        public Task ExecuteAsync(IReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            var command = _commandFactory(context);
//            context.ProcessManager.EnqueueOutgoingCommand(command);
//            return Task.CompletedTask;
//        }

//        public void SendTelemetry(ITelemetryContext context)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}