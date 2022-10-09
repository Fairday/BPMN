using QuikGraph;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal interface IInternalEdge<out TVertex> : IEdge<TVertex>
        where TVertex : IVertex
    {
        bool IsProduced { get; }
        bool IsDisabled { get; }
        bool IsAborted { get; }
    }
}