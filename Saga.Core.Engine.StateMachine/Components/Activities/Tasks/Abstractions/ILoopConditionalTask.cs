using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ILoopConditionalTask<TProcess> : IProcess<Nothing>
        where TProcess : IProcess<Nothing>
    {
        TProcess Task { get; }
        int Attempts { get; }
        Func<TProcess, bool> BreakCondition { get; }
    }
}