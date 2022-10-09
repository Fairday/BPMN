namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IDataBasedParallelProcessGateway<TInput1, TInput2> :
        IProcessGateway<IDataBasedParallelProcessGateway<TInput1, TInput2>>,
        IAcceptData<TInput1, TInput2>,
        ITransmitData<TInput1, TInput2>
    {
    }
}