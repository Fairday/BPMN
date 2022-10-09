using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class DirectedGraphBuilder : IGraphBuilder
    {
        private bool _graphBuilt;
        private readonly Graph _graph;

        public DirectedGraphBuilder()
        {
            _graph = new Graph();
        }

        public void AddEdge(string v, string w)
        {
            if (_graphBuilt)
                throw new InvalidOperationException("The graph have been already built");

            _graph.AddEdge(v, w);
        }

        public IGraph Build()
        {
            _graphBuilt = true;
            return _graph;
        }
    }
}