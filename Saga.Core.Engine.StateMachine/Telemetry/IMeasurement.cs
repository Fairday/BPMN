namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IMeasurement
    {
        string Name { get; }
        string Value { get; }
    }
}