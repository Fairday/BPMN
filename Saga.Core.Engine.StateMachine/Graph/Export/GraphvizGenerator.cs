using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;
using QuikGraph.Graphviz;
using QuikGraph.Graphviz.Dot;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Graph
{
    internal sealed class GraphvizGenerator<TProcessManager>
        where TProcessManager : IProcessManager<TProcessManager>
    {
        private readonly IProcessManager<TProcessManager> _processManager;
        private AdjacencyGraph<IVertex, IInternalEdge<IVertex>> _graph;

        public GraphvizGenerator(IProcessManager<TProcessManager> processManager)
        {
            _processManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
            _graph = CreateAdjacencyGraph(_processManager);
        }

        public string GenerateDotDefinition()
        {
            var algorithm = new GraphvizAlgorithm<IVertex, IInternalEdge<IVertex>>(_graph);
            algorithm.FormatVertex += VertexStyleFormatter;
            algorithm.FormatEdge += EdgeStyleFormatter;
            return algorithm.Generate();
        }

        public GraphvizGenerator<TProcessManager> RebuildAdjacencyGraph()
        {
            _graph = CreateAdjacencyGraph(_processManager);
            return this;
        }

        private void VertexStyleFormatter(object sender, FormatVertexEventArgs<IVertex> args)
        {
            args.VertexFormat.Label = args.Vertex.Name;
            switch (args.Vertex.Type)
            {
                case VertexType.Event:
                {
                    args.VertexFormat.Shape = GraphvizVertexShape.Diamond;
                    var eventVertex = (EventVertex)args.Vertex;

                    var @event = eventVertex.Event;
                    if (eventVertex.IsAborted)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.OrangeRed;
                    }
                    else if (eventVertex.IsDisabled)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.LightGray;
                    }
                    else if(@event.IsPropagated)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.LightGreen;
                    }

                    break;
                }
                case VertexType.State:
                {
                    args.VertexFormat.Shape = GraphvizVertexShape.Box;
                    var stateVertex = (StateVertex)args.Vertex;

                    var state = stateVertex.State;
                    if (stateVertex.IsAborted)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.OrangeRed;
                    }
                    else if (stateVertex.ProcessStarted && state.IsActive)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.Yellow;
                    }
                    else if (state.IsDisabled)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.LightGray;
                    }
                    else if (state.Leaved.IsPropagated)
                    {
                        args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = GraphvizColor.LightGreen;
                    }

                    break;
                }
                case VertexType.Dummy:
                {
                    args.VertexFormat.Shape = GraphvizVertexShape.Ellipse;
                    var dummyVertex = (DummyVertex)args.Vertex;

                    switch (dummyVertex.Category)
                    {
                        case DummyVertex.DummyVertexCategory.In:
                            args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                            args.VertexFormat.FillColor = GraphvizColor.LightGray;
                            break;
                        case DummyVertex.DummyVertexCategory.Out:
                            args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                            args.VertexFormat.FillColor = GraphvizColor.LightGray;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
            }
        }

        private void EdgeStyleFormatter(object sender, FormatEdgeEventArgs<IVertex, IInternalEdge<IVertex>> args)
        {
            if (args.Edge.IsAborted)
                args.EdgeFormat.StrokeColor = GraphvizColor.DarkRed;
            else if (args.Edge.IsDisabled)
                args.EdgeFormat.StrokeColor = GraphvizColor.DarkGray;
            else if(args.Edge.IsProduced)
                args.EdgeFormat.StrokeColor = GraphvizColor.DarkGreen;
        }

        private AdjacencyGraph<IVertex, IInternalEdge<IVertex>> CreateAdjacencyGraph(IProcessManager<TProcessManager> sourceGraph)
        {
            var graph = new AdjacencyGraph<IVertex, IInternalEdge<IVertex>>();

            IDictionary<string, IVertex> vertices = new Dictionary<string, IVertex>();

            foreach (var state in sourceGraph.States)
            {
                var stateVertex = new StateVertex(state, sourceGraph.IsStarted, sourceGraph.IsAborted);
                graph.AddVertex(stateVertex);
                vertices[state.Name] = stateVertex;
            }

            var dummyVertex = new DummyVertex("In", DummyVertex.DummyVertexCategory.In);
            graph.AddVertex(dummyVertex);
            vertices[dummyVertex.Name] = dummyVertex;
            foreach (var state in sourceGraph.InitialStates)
            {
                var edge = new GeneralEdge(dummyVertex, vertices[state.Name], sourceGraph.IsStarted, false, false);
                graph.AddEdge(edge);
            }

            foreach (var state in sourceGraph.States)
            {
                foreach (var @event in state.LeavePrerequisites)
                {
                    if (Equals(@event, state.Entered))
                        continue;

                    var eventVertex = new EventVertex(@event, state.IsDisabled, sourceGraph.IsAborted);
                    graph.AddVertex(eventVertex);

                    var edge = new GeneralEdge(eventVertex, vertices[state.Name], @event.IsPropagated, state.IsDisabled, eventVertex.IsAborted);
                    graph.AddEdge(edge);
                }

                foreach (var outward in state.LeaveTransitions)
                {
                    var edge = new TransitionEdge(outward, vertices[outward.From.Name], vertices[outward.To.Name], sourceGraph.IsAborted);
                    graph.AddEdge(edge);
                }
            }

            dummyVertex = new DummyVertex("Out", DummyVertex.DummyVertexCategory.In);
            graph.AddVertex(dummyVertex);
            vertices[dummyVertex.Name] = dummyVertex;
            foreach (var state in sourceGraph.States.Where(s => s.IsFinal))
            {
                var edge = new GeneralEdge(vertices[state.Name], dummyVertex, state.Leaved.IsPropagated, false, vertices[state.Name].IsAborted);
                graph.AddEdge(edge);
            }

            return graph;
        }
    }
}