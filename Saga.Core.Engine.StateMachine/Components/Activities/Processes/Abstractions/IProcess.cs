using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcess<TInput> : IFlowObject, IActivity, IHandleError, IAcceptData<TInput>, IConsumingMessages
    {
        ITerminationEvent Finish { get; }
        ITerminationEvent EscalateError { get; }
        IEnumerable<IProcessEvent> AttachedEvents { get; }
        IEnumerable<IFlowElement> Units { get; }
    }
}