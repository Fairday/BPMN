using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components.Data.Abstractions
{
    public interface IDataHolder<TData> : IFlowElement
    {
        TData Data { get; }
    }
}