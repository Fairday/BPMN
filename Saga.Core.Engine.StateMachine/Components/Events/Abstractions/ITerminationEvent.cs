namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ITerminationEvent : IProcessEvent, IEndEvent
    {
        IFlowElement BoundedFlowElement { get; }
        TerminationBehavior TerminationBehavior { get; }
        ITerminationHandler TerminationHandler { get; }
    }

    public interface ITerminationEvent<TResult> : IProcessEvent, IEndEvent, IAcceptData<TResult>
    {
        IFlowElement BoundedFlowElement { get; }
        TerminationBehavior TerminationBehavior { get; }
        ITerminationHandler<TResult> TerminationHandler { get; }
    }
}