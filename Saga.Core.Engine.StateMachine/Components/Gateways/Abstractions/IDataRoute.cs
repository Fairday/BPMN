namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IDataRoute<TInput>
    {
        IAcceptData<TInput> Acceptor { get; }
    }
}