namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IConnectionObject : IFlowElement
    {
        IFlowElement From { get; }
        IFlowElement To { get; }
    }

    public interface IConnectingObject<TData> : IConnectionObject, IFlowElement, IAcceptData<TData>, ITransmitData<TData>
    {
    }
}