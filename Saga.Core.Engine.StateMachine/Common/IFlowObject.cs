namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IFlowObject : IFlowElement
    {
        ITopology Topology { get; }
        IProcessUnitStorage Storage { get; }
    }
}