namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ICatchingMessageEvent<TCatchingMessage> : IProcessEvent, IStartEvent, IIntermediateEvent, IConsumingMessages
        where TCatchingMessage : class
    {
        TCatchingMessage CaughtMessage { get; }
    }
}