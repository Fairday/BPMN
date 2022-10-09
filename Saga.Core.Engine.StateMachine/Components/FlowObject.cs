using OrchestratoR.Core.Components;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public abstract class FlowObject : FlowElement, IFlowObject
    {
        protected FlowObject(string name) : base(name)
        {
            EventListener.SubscribeToAllSpecificEvents<IFinishedEvent>(e =>
            {
                Topology.Resolve(e.Source);
            });
        }

        public ITopology Topology { get; }
        public IProcessUnitStorage Storage { get; }
        protected ITopologyTraverser TopologyTraverser { get; }
    }
}