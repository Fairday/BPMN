using System;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAsyncActionTaskWithInputAndResult<TInput, TResult> : IProcessWithResult<TInput, TResult>
    {
        Func<TInput, Task<TResult>> Action { get; }
    }
}