using System;
using Saga.Core.Engine.StateMachine.Abstractions;
using Saga.Core.Engine.StateMachine.Components.Enums;

namespace Saga.Core.Engine.StateMachine.Components.Events
{
    public interface ICatchingMessageConditionalEvent<TCatchingMessage> : IProcessEvent, IStartEvent, IIntermediateEvent, IConsumingMessages
        where TCatchingMessage : class
    {
        FalseConditionalBehavior FalseConditionalBehavior { get; }
        IConnection False { get; }
        Func<TCatchingMessage, bool> CaughtCondition { get; }
    }
}