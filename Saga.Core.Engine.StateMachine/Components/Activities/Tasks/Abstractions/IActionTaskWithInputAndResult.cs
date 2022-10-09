using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IActionTaskWithInputAndResult<TInput, TResult> : IProcessWithResult<TInput, TResult>
    {
        Func<TInput, TResult> Action { get; }
    }
}