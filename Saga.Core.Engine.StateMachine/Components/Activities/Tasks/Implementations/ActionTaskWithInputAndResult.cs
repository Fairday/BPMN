using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ActionTaskWithInputAndResult<TInput, TResult> : ProcessWithResultBase<TInput, TResult>, IActionTaskWithInputAndResult<TInput, TResult>
    {
        public ActionTaskWithInputAndResult(string name) : base(name)
        {
            After(this)
                .ThenClarifyType(this)
                .ExtractData(u => u.Result)
                .TransmitToTerminatation(Finish);

            AfterError(ErrorEvent)
                .ThenTerminateAt(EscalateError);
        }

        public Func<TInput, TResult> Action { get; }

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
                Result = Action(Data);
            }
            catch (Exception e)
            {
                ErrorEvent.ImpartErrorObject(e);
                return ActivityExecutionStatus.Faulted;
            }

            return ActivityExecutionStatus.Completed;
        }
    }
}