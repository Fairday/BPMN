namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IGraphValidator
    {
        bool Validate(IGraph graph);
    }
}