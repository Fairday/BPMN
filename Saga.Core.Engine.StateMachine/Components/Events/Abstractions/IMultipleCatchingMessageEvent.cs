namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IMultipleCatchingMessageEvent<out TMessage1, out TMessage2> : IProcessEvent
        where TMessage1 : class
        where TMessage2 : class
    {
        MultipleEventCatchingType MultipleEventCatchingType { get; }
        TMessage1 CaughtMessage1 { get; }
        TMessage2 CaughtMessage2 { get; }
    }

    public interface IMultipleCatchingMessageEvent<out TMessage1, out TMessage2, out TMessage3> : IProcessEvent
        where TMessage1 : class
        where TMessage2 : class
        where TMessage3 : class
    {
        MultipleEventCatchingType MultipleEventCatchingType { get; }
        TMessage1 CaughtMessage1 { get; }
        TMessage2 CaughtMessage2 { get; }
        TMessage3 CaughtMessage3 { get; }
    }

    public interface IMultipleCatchingMessageEvent<out TMessage1, out TMessage2, out TMessage3, out TMessage4> : IProcessEvent
        where TMessage1 : class
        where TMessage2 : class
        where TMessage3 : class
    {
        MultipleEventCatchingType MultipleEventCatchingType { get; }
        TMessage1 CaughtMessage1 { get; }
        TMessage2 CaughtMessage2 { get; }
        TMessage3 CaughtMessage3 { get; }
        TMessage4 CaughtMessage4 { get; }
    }
}