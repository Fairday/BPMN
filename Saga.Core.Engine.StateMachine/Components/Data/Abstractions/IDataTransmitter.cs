using Saga.Core.Engine.StateMachine;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components.Data.Abstractions
{
    public interface IDataTransmitter<TData> : IFlowElement, ITransmitData<TData> 
    {
        IDataHolder<TData> DataExtractor { get; }
    }
}