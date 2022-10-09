using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IDataAggregator<TAggregation> : IFlowElement, IAcceptData<object[]>
    {
        Func<object[], TAggregation> Aggregate { get; }
    }

    public interface IDataAggregator<TInput1, TInput2, TAggregation> : IFlowElement, IAcceptData<IAll<TInput1 ,TInput2>>
    {
        Func<TInput1, TInput2, TAggregation> Aggregate { get; }
    }

    //public interface IDataAggregator<TInput1, TInput2, TInput3, TAggregation> : IProcessUnit, IAcceptData<IAll<TInput1, TInput2, TInput3>>
    //{
    //    Func<TInput1, TInput2, TInput3, TAggregation> Aggregate { get; }
    //}

    //public interface IDataAggregator<TInput1, TInput2, TInput3, TInput4, TAggregation> : IProcessUnit, IAcceptData<TInput1, TInput2, TInput3, TInput4>
    //{
    //    Func<TInput1, TInput2, TInput3, TInput4, TAggregation> Aggregate { get; }
    //}

    //public interface IDataAggregator<TInput1, TInput2, TInput3, TInput4, TInput5, TAggregation> : IProcessUnit, IAcceptData<TInput1, TInput2, TInput3, TInput4, TInput5>
    //{
    //    Func<TInput1, TInput2, TInput3, TInput4, TInput5, TAggregation> Aggregate { get; }
    //}

    //public interface IDataAggregator<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TAggregation> : IProcessUnit, IAcceptData<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6>
    //{
    //    Func<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TAggregation> Aggregate { get; }
    //}

    //public interface IDataAggregator<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TInput7, TAggregation> : IProcessUnit, IAcceptData<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TInput7>
    //{
    //    Func<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TInput7, TAggregation> Aggregate { get; }
    //}

    //public interface IDataAggregator<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TInput7, TInput8, TAggregation> : IProcessUnit, IAcceptData<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TInput7, TInput8>
    //{
    //    Func<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6, TInput7, TInput8, TAggregation> Aggregate { get; }
    //}
}