using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ISendMessageTask<TSendingMessage> : IProcess<Nothing>
        where TSendingMessage : class
    {
        Func<TSendingMessage> SendingMessageFactory { get; }
    }

    public interface ISendMessageTask<TInput, TSendingMessage> : IProcess<TInput>
        where TSendingMessage : class
    {
        Func<TInput, TSendingMessage> SendingMessageFactory { get; }
    }
}