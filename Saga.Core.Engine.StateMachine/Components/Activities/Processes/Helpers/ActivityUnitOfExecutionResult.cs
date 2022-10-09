using System.Runtime.CompilerServices;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public sealed class ActivityUnitOfExecutionResult
    {
        private ActivityUnitOfExecutionResult(ActivityUnitOfExecutionStatus status)
        {
            Status = status;
        }

        public ActivityUnitOfExecutionStatus Status { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityUnitOfExecutionResult Completed() => new ActivityUnitOfExecutionResult(ActivityUnitOfExecutionStatus.Completed);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityUnitOfExecutionResult InProgress() => new ActivityUnitOfExecutionResult(ActivityUnitOfExecutionStatus.InProgress);
    }

    public sealed class ActivityUnitOfExecutionResult<TResult>
    {
        private ActivityUnitOfExecutionResult(ActivityUnitOfExecutionStatus status, TResult result)
        {
            Status = status;
            Result = result;
        }

        public ActivityUnitOfExecutionStatus Status { get; }
        public TResult Result { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityUnitOfExecutionResult<TResult> Completed(TResult result)
            => new ActivityUnitOfExecutionResult<TResult>(ActivityUnitOfExecutionStatus.Completed, result);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityUnitOfExecutionResult<TResult> InProgress()
            => new ActivityUnitOfExecutionResult<TResult>(ActivityUnitOfExecutionStatus.InProgress, default(TResult));
    }
}