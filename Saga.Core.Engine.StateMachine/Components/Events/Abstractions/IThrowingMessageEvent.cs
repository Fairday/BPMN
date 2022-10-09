using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IThrowingMessageEvent<out TThrowingMessage> : IProcessEvent
        where TThrowingMessage : class
    {
        Func<TThrowingMessage> ThrowingMessageFactory { get; }
    }
}