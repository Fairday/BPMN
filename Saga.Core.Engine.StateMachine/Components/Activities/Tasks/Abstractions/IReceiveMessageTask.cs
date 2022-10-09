namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IReceiveMessageTask<out TReceivedMessage> : IProcess<Nothing>
        where TReceivedMessage : class
    {
        TReceivedMessage ReceivedMessage { get; }
    }
}