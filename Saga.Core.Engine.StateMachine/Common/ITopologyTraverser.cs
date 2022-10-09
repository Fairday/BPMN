using System.Collections.Generic;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components
{
    public interface ITopologyTraverser
    {
        IEnumerable<IFlowElement> Traverse(ITopology topology);
    }
}