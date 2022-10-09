namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public enum ProcessEventLifetime
    {
        Start = 0,
        Intermediate = 1,
        End = 2
    }

    public interface IStartEvent
    {

    }

    public interface IIntermediateEvent
    {

    }

    public interface IEndEvent
    {

    }
}