namespace Saga.Core.Engine.StateMachine.Graph
{
    internal interface IVertex
    {
        VertexType Type { get; }
        string Name { get; }
        bool IsAborted { get; }
    }
}