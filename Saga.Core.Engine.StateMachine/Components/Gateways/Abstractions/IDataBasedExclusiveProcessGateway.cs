namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IExclusiveProcessGateway : IProcessGateway<IExclusiveProcessGateway>
    {
    }

    public interface IParallelProcessGateway : IProcessGateway<IParallelProcessGateway>
    {
    }

    public interface IDataBasedExclusiveProcessGateway<TInput1, TInput2> :
        IProcessGateway<IDataBasedExclusiveProcessGateway<TInput1, TInput2>>,
        IAcceptData<IAny<TInput1, TInput2>>,
        ITransmitData<IAny<TInput1, TInput2>>
    {
    }

    public interface IDataBasedExclusiveProcessGateway<TInput1, TInput2, TInput3> :
        IProcessGateway<IDataBasedExclusiveProcessGateway<TInput1, TInput2, TInput3>>,
        IAcceptData<TInput1, TInput2, TInput3>,
        ITransmitData<IAny<TInput1, TInput2, TInput3>>
    {
    }

    public interface IDataBasedExclusiveProcessGateway<TInput1, TInput2, TInput3, TInput4> :
        IProcessGateway<IDataBasedExclusiveProcessGateway<TInput1, TInput2, TInput3, TInput4>>,
        IAcceptData<TInput1, TInput2, TInput3, TInput4>,
        ITransmitData<IAny<TInput1, TInput2, TInput3, TInput4>>
    {
    }

    public interface IDataBasedRouteExclusiveProcessGateway<TInput1, TInput2> :
        IProcessGateway<IDataBasedExclusiveProcessGateway<TInput1, TInput2>>,
        IAcceptData<TInput1, TInput2>,
        ITransmitData<IAny<TInput1, TInput2>>
    {
        IDataRoute<TInput1> Route1 { get; }
        IDataRoute<TInput2> Route2 { get; }
    }

    public interface IDataBasedRouteExclusiveProcessGateway<TInput1, TInput2, TInput3> :
        IProcessGateway<IDataBasedExclusiveProcessGateway<TInput1, TInput2, TInput3>>,
        IAcceptData<TInput1, TInput2, TInput3>,
        ITransmitData<IAny<TInput1, TInput2, TInput3>>
    {
        IDataRoute<TInput1> Route1 { get; }
        IDataRoute<TInput2> Route2 { get; }
        IDataRoute<TInput3> Route3 { get; }
    }

    public interface IDataBasedRouteExclusiveProcessGateway<TInput1, TInput2, TInput3, TInput4> : 
        IProcessGateway<IDataBasedExclusiveProcessGateway<TInput1, TInput2, TInput3, TInput4>>,
        IAcceptData<TInput1, TInput2, TInput3, TInput4>,
        ITransmitData<IAny<TInput1, TInput2, TInput3, TInput4>>
    {
        IDataRoute<TInput1> Route1 { get; }
        IDataRoute<TInput2> Route2 { get; }
        IDataRoute<TInput3> Route3 { get; }
        IDataRoute<TInput4> Route4 { get; }
    }
}