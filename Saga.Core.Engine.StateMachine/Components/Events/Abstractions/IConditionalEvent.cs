using System;
using Saga.Core.Engine.StateMachine.Abstractions;
using Saga.Core.Engine.StateMachine.Components.Enums;

namespace Saga.Core.Engine.StateMachine.Components.Events
{
    public interface IConditionalEvent : IProcessEvent
    {
        FalseConditionalBehavior FalseConditionalBehavior { get; }
        IOutputConnection False { get; }
        Func<bool> Condition { get; }
    }
}