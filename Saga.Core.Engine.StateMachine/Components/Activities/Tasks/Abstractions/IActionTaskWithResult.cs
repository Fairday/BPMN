using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IActionTaskWithResult<TResult> : IProcessWithResult<Nothing, TResult>
    {
        Func<TResult> Action { get; }
    }
}