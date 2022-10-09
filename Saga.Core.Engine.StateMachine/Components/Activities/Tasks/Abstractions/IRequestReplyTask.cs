using System;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Components.Tasks
{
    public interface IRequestReplyTask<out TRequestMessage, TReplyMessage> : IProcess<Nothing>
        where TRequestMessage : class
        where TReplyMessage : class
    {
        Func<TRequestMessage> RequestMessageFactory { get; }
        TReplyMessage ReplyMessage { get; }
    }

    public interface IRequestReplyTask<TInput, out TRequestMessage, TReplyMessage> : IProcess<TInput>
        where TRequestMessage : class
        where TReplyMessage : class
    {
        Func<TRequestMessage> RequestMessageFactory { get; }
        TReplyMessage ReplyMessage { get; }
    }
}