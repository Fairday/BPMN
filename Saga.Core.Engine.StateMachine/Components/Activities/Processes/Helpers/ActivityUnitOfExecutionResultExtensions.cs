using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public static class ActivityUnitOfExecutionResultExtensions
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ActivityUnitOfExecutionResult> AsTask(this ActivityUnitOfExecutionResult result) 
            => Task.FromResult(result);
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<ActivityUnitOfExecutionResult<TResult>> AsTask<TResult>(this ActivityUnitOfExecutionResult<TResult> result)
            => Task.FromResult(result);
    }
}