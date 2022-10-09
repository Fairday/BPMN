using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ActionTaskWithInput<TInput> : ProcessBase<TInput>, IActionTaskWithInput<TInput>
    {
        public ActionTaskWithInput(string name) : base(name)
        {
            After(Complete)
                .ThenTerminateAt(Finish);

            AfterError(ErrorEvent)
                .ThenTerminateAt(EscalateError);
        }

        public Action<TInput> Action { get; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override Task ConsumeMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken) => Task.CompletedTask;

        public override async Task ActivateAsync(CancellationToken cancellationToken)
        {
            IsActive = true;
            Status = await TryExecuteAsync(cancellationToken);
            switch (Status)
            {
                case ActivityExecutionStatus.Faulted:
                    {
                        await ErrorEvent.ActivateAsync(cancellationToken);
                        break;
                    }
                case ActivityExecutionStatus.Completed:
                    {
                        await Finish.ActivateAsync(cancellationToken);
                        break;
                    }
            }
        }

        public override Task DisableAsync(CancellationToken cancellationToken)
        {
            IsDisabled = true;
            return Task.CompletedTask;
        }


        public override Task<ActivityExecutionStatus> TryExecuteAsync(CancellationToken cancellationToken)
        {
            var executionStatus = TryExecuteAsyncCore();
            return Task.FromResult(executionStatus);
        }

        private ActivityExecutionStatus TryExecuteAsyncCore()
        {
            try
            {
                Action(Data);
            }
            catch (Exception e)
            {
                ErrorEvent.ImpartErrorObject(e);
                return ActivityExecutionStatus.Faulted;
            }

            return ActivityExecutionStatus.Completed;
        }

        public override Task CancelAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}