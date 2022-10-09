using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ITopology
    {
        IReadOnlyDictionary<IFlowElement, ISet<IFlowElement>> Adjacency { get; }
        IReadOnlyDictionary<IFlowElement, ISet<IFlowElement>> Prerequisites { get; }
        IReadOnlyCollection<IFlowElement> Units { get; }
        IReadOnlyCollection<IConnectionObject> Connections { get; }
        IReadOnlyCollection<IFlowElement> Current { get; }
        IReadOnlyCollection<IFlowElement> GetOpenAdjacencies(IFlowElement processUnit);
        void Resolve(IFlowElement processUnit);
    }
}