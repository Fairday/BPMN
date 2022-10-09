using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ProcessGatewayProxy : IProcessGateway, IIngestSensor
    {
        public ProcessGateway Source { get; }

        public ProcessGatewayProxy(
            Expression<Func<IEnumerable<IConnection>, bool>> isSynchronizedExpression,
            Expression<Func<IEnumerable<IConnection>, IEnumerable<IConnection>>> evaluateExpression,
            IEnumerable<IConnection> inputConnections,
            IEnumerable<IConnection> outputConnections)
        {
            Source = new ProcessGateway(
                isSynchronizedExpression,
                evaluateExpression,
                inputConnections,
                outputConnections);
        }

        public Task Accept(IProcessVisitor visitor)
        {
            return Source.Accept(visitor);
        }

        public bool Equals(IFlowElement other)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public bool IsCompleted { get; }
        public bool IsActive { get; }
        public bool IsDisabled { get; }

        public Task ActivateAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConnection> InputConnections { get; }
        public IEnumerable<IConnection> OutputConnections { get; }
        public Expression<Func<IEnumerable<IConnection>, bool>> IsSynchronizedExpression { get; }
        public Expression<Func<IEnumerable<IConnection>, IEnumerable<IConnection>>> EvaluateExpression { get; }
        public void IngestSensor(ISensor sensor)
        {
            throw new NotImplementedException();
        }
    }
}