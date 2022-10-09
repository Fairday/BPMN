namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IFinishedEvent : IInternalEvent
    {
    }

    public interface IFinishedEvent<TResult> : IInternalEvent
    {
        TResult Result { get; }
    }
}