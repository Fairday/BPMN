using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessEventListener
    {
        void SubscribeToAllEvents(Action<IInternalEvent> onRaise);
        void SubscribeToAllEventsAsync(Func<IInternalEvent, CancellationToken, Task> onRaise);
        void SubscribeToAllSpecificEvents<TInternalEvent>(Action<TInternalEvent> onRaise)
            where TInternalEvent : IInternalEvent;
        void SubscribeToAllSpecificEventsExceptSources<TInternalEvent>(Action<TInternalEvent> onRaise, params IFlowElement[] exceptSource)
            where TInternalEvent : IInternalEvent;
        void SubscribeToAllSpecificEventsAsync<TInternalEvent>(Func<TInternalEvent, CancellationToken, Task> onRaise)
            where TInternalEvent : IInternalEvent;
        void SubscribeToAllSpecificEventsExceptSourcesAsync<TInternalEvent>(Func<TInternalEvent, CancellationToken, Task> onRaise, params IFlowElement[] exceptSource)
            where TInternalEvent : IInternalEvent;
        void SubscribeToSpecificEventFromSource<TInternalEvent>(IFlowElement source, Action<TInternalEvent> onRaise)
            where TInternalEvent : IInternalEvent;
        void SubscribeToSpecificEventFromSourceAsync<TInternalEvent>(IFlowElement source, Func<TInternalEvent, CancellationToken, Task> onRaise)
            where TInternalEvent : IInternalEvent;
    }
}