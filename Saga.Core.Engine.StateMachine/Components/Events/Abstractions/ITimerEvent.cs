using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ITimerEvent : IProcessEvent, IStartEvent, IIntermediateEvent, IEndEvent
    {
        DateTimeOffset? StartTime { get; }
        TimeSpan Delay { get; }
    }
}