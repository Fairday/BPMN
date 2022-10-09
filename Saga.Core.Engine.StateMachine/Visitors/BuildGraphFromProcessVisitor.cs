using System;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class BuildGraphFromProcessVisitor : IProcessVisitor
    {
        private readonly IGraphBuilder _graphBuilder;

        public BuildGraphFromProcessVisitor(IGraphBuilder graphBuilder)
        {
            _graphBuilder = graphBuilder ?? throw new ArgumentNullException(nameof(graphBuilder));
        }

        public Task Visit<TProcessManager>(ProcessManager<TProcessManager> processManager) 
            where TProcessManager : IProcessManager<TProcessManager>
        {
            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IState<TProcessManager> state) 
            where TProcessManager : IProcessManager<TProcessManager>
        {
            return Task.CompletedTask;
        }

        public Task Visit(IConnection connection)
        {
            if (connection == null) 
                throw new ArgumentNullException(nameof(connection));

            var from = connection.From.Name;
            var to = connection.To.Name;
            _graphBuilder.AddEdge(from, to);
            return Task.CompletedTask;
        }

        public Task Visit(IProcessEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IBehavior<TProcessManager> behavior)
            where TProcessManager : IProcessManager<TProcessManager>
        {
            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IActivity<TProcessManager> state) 
            where TProcessManager : IProcessManager<TProcessManager>
        {
            return Task.CompletedTask;
        }
    }
}