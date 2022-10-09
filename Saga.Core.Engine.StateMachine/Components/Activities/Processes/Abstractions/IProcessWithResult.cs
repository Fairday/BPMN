using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessWithResult<TInput, TResult> : IFlowObject, IActivity<TResult>, IHandleError, IAcceptData<TInput>, IConsumingMessages
    {
        ITerminationEvent<TResult> Finish { get; }
        ITerminationEvent EscalateError { get; }
        IEnumerable<IProcessEvent> AttachedEvents { get; }
        IEnumerable<IFlowElement> Units { get; }
    }
}