namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IGraphBuilder
    {
        void AddEdge(string v1, string v2);
        IGraph Build();
    }
}