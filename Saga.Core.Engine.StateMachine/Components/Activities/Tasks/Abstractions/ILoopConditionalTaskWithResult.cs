using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ILoopConditionalTaskWithResult<TProcess, TResult> : IProcessWithResult<Nothing, TResult>
        where TProcess : IProcessWithResult<Nothing, TResult>
    {
        TProcess Task { get; }
        int Attempts { get; }
        Func<TProcess, bool> BreakCondition { get; }
    }
}