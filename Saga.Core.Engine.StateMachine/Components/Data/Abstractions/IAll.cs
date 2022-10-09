namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAll<TData1, TData2>
    {
        TData1 Data1 { get; }
        TData2 Data2 { get; }
    }
}