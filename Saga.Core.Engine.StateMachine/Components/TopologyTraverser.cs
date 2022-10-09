using System.Collections.Generic;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components
{
    internal sealed class TopologyTraverser : ITopologyTraverser
    {
        public IEnumerable<IFlowElement> Traverse(ITopology topology)
        {
            var queue = new Queue<IFlowElement>();

            HashSet<IFlowElement> alreadyEnqueued = new();

            foreach (var current in topology.Current)
                if (alreadyEnqueued.Add(current))
                    queue.Enqueue(current);

            while (queue.Count > 0)
            {
                var unit = queue.Dequeue();
                yield return unit;
                if (unit.IsCompleted)
                {
                    topology.Resolve(unit);
                    foreach (var openedUnit in topology.GetOpenAdjacencies(unit))
                    {
                        if (alreadyEnqueued.Add(openedUnit))
                            queue.Enqueue(openedUnit);
                    }
                }
            }
        }
    }
}