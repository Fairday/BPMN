using System;
using System.Collections.Generic;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components
{
    internal sealed class Topology : ITopology
    {
        public IReadOnlyDictionary<IFlowElement, ISet<IFlowElement>> Adjacency { get; }
        public IReadOnlyDictionary<IFlowElement, ISet<IFlowElement>> Prerequisites { get; }
        public IReadOnlyCollection<IFlowElement> Units { get; }
        public IReadOnlyCollection<IConnectionObject> Connections { get; }
        public IReadOnlyCollection<IFlowElement> Current { get; }

        public void Resolve(IFlowElement flowElement)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<IFlowElement> GetOpenAdjacencies(IFlowElement flowElement)
        {
            throw new NotImplementedException();
        }
    }
}