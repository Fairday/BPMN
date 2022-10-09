namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IGraphValidationRule
    {
        string ReasonIfNotSatisfied { get; }
        bool Check(IGraph graph);
    }
}