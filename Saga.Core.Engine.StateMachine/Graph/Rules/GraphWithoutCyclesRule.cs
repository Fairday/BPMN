using System;
using System.Collections.Generic;
using System.Linq;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class GraphWithoutCyclesRule : IGraphValidationRule
    {
        public string ReasonIfNotSatisfied => throw new NotImplementedException();

        public bool Check(IGraph graph)
        {
            if (graph == null) 
                throw new ArgumentNullException(nameof(graph));

            // ReSharper disable once IdentifierTypo
            var indegree = new Dictionary<string, int>();
            foreach (var (vertex, outwardEdges) in graph.Adjacency)
            {
                if (!indegree.ContainsKey(vertex))
                    indegree[vertex] = 0;
                foreach (var outwardEdge in outwardEdges)
                {
                    if (indegree.ContainsKey(outwardEdge))
                        indegree[outwardEdge]++;
                    else
                        indegree[outwardEdge] = 1;
                }
            }

            var queue = new Queue<string>();
            foreach (var vertexInfo in indegree.Where(vi => vi.Value == 0))
                queue.Enqueue(vertexInfo.Key);

            var vertices = indegree.Count;

            while (queue.Count > 0)
            {
                var passed = queue.Dequeue();
                vertices--;
                foreach (var vertex in graph.Adjacency[passed])
                    if (--indegree[vertex] == 0)
                        queue.Enqueue(vertex);
            }

            return vertices == 0;
        }
    }
}