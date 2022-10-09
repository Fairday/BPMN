using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public abstract class ProcessWithResult<TData, TResult> : Activity<TResult>, IProcessWithResult<TData, TResult>
    {
        protected ProcessWithResult(string name) : base(name)
        {
            Status = ActivityExecutionStatus.NotStarted;

            EventListener.SubscribeToSpecificEventFromSourceAsync<IFinishedEvent>(Activated, async (e, token) =>
            {
                Status = await TryExecuteAsync(token);
                switch (Status)
                {
                    case ActivityExecutionStatus.Faulted:
                    {
                        await ErrorEvent.ActivateAsync(token);
                        break;
                    }
                    case ActivityExecutionStatus.Completed:
                    {
                        await Completed.ActivateAsync(token);
                        break;
                    }
                    case ActivityExecutionStatus.Canceled:
                    {
                        await Canceled.ActivateAsync(token);
                        break;
                    }
                }
            });

            EventListener.SubscribeToSpecificEventFromSourceAsync<IFinishedEvent>(Canceled, async (e, token) =>
            {
                await OnCancelAsync(token);
                Status = ActivityExecutionStatus.Canceled;
                IsCanceled = true;
            });

            AfterError(ErrorEvent)
                .ThenTerminateAt(EscalateError);
        }

        public IErrorEvent ErrorEvent { get; }
        public ITerminationEvent<TResult> Finish { get; }
        public ITerminationEvent EscalateError { get; }
        public IEnumerable<IProcessEvent> AttachedEvents { get; }
        public IEnumerable<IFlowElement> Units { get; }
        public TData Data { get; internal set; }
        public TResult Result { get; protected internal set; }

        public sealed override async Task ActivateAsync(CancellationToken cancellationToken)
        { 
            await Activated.ActivateAsync(cancellationToken);
        }

        public sealed override async Task DisableAsync(CancellationToken cancellationToken)
        {
            await Disabled.ActivateAsync(cancellationToken);
        }

        public void AcceptData(TData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        protected abstract Task<ActivityUnitOfExecutionResult<TResult>> OnExecuteAsync(CancellationToken cancellationToken);
        protected abstract Task OnCancelAsync(CancellationToken cancellationToken);

        private async Task<ActivityExecutionStatus> TryExecuteAsync(CancellationToken cancellationToken)
        {
            if (Status != ActivityExecutionStatus.NotStarted)
                return Status;

            try
            {
                var activationResultType = await OnExecuteAsync(cancellationToken);
                switch (activationResultType.Status)
                {
                    case ActivityUnitOfExecutionStatus.InProgress:
                    {
                        return ActivityExecutionStatus.Running;
                    }
                    case ActivityUnitOfExecutionStatus.Completed:
                    {
                        return ActivityExecutionStatus.Completed;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(activationResultType), activationResultType, "Unable to handle ActivationResultType {activationResultType}");
                } 
            }
            catch (Exception e)
            {
                ErrorEvent.ImpartErrorObject(e);
                return ActivityExecutionStatus.Faulted;
            }
        }

        public Task ConsumeMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken) where TMessage : class
        {
            throw new NotImplementedException();
        }
    }
}