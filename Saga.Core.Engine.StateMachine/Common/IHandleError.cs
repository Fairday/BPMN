namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IHandleError
    {
        IErrorEvent ErrorEvent { get; }
    }
}