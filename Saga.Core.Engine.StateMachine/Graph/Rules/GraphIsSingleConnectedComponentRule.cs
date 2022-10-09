using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Graph.Rules
{
    //use dsu
    internal sealed class GraphIsSingleConnectedComponentRule : IGraphValidationRule
    {
        public string ReasonIfNotSatisfied { get; }
        public bool Check(IGraph graph)
        {
            throw new System.NotImplementedException();
        }
    }
}