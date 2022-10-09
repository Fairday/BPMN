using Seedwork.Functional.MaybeModel;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAny<TData1, TData2>
    {
        Maybe<TData1> Data1 { get; }
        Maybe<TData2> Data2 { get; }
    }

    public interface IAny<TData1, TData2, TData3> : IAny<TData1, TData2>
    {
        Maybe<TData3> Data3 { get; }
    }

    public interface IAny<TData1, TData2, TData3, TData4> : IAny<TData1, TData2, TData3>
    {
        Maybe<TData4> Data4 { get; }
    }
}