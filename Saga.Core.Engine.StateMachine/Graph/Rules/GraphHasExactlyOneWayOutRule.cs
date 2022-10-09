using System;
using System.Linq;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class GraphHasExactlyOneWayOutRule : IGraphValidationRule
    {
        public string ReasonIfNotSatisfied => throw new NotImplementedException();

        public bool Check(IGraph graph)
        {
            if (graph == null) 
                throw new ArgumentNullException(nameof(graph));

            return graph.Adjacency.Count(a => a.Value.Count == 0) == 1;
        }
    }
}