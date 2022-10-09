using System;
using System.Collections.Generic;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Components.Processes
{
    public interface IAdhocProcessWithResult<TResult> : IProcessWithResult<TResult>
    {
        ActivityExecutionBehavior ExecutionBehavior { get; }
        IEnumerable<IProcess> Tasks { get; }
        int Attempts { get; }
        Func<bool> BreakCondition { get; }
    }
}