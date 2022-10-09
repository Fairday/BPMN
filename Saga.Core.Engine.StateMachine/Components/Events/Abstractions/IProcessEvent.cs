namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessEvent : IFlowElement
    {
        ProcessEventLifetime Lifetime { get; }
        ActivityBoundaryBehavior BoundaryBehavior { get; }
        ITerminationEvent Finish { get; }
    }
}