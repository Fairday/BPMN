namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IInternalEvent
    {
        IFlowElement Source { get; }
    }
}