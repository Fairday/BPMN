using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public class ProcessGatewayBuilder
    {
        private readonly HashSet<IFlowElement> _sources = new();
        private readonly HashSet<IFlowElement> _destinations = new();
        private Expression<Func<IEnumerable<IConnection>, bool>> _isSynchronizedExpression;
        private Expression<Func<IEnumerable<IConnection>, IEnumerable<IConnection>>> _evaluateExpression;

        public ProcessGatewayBuilder AddConnectionFrom(IFlowElement source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            _sources.Add(source);
            return this;
        }

        public ProcessGatewayBuilder AddConnectionTo(IFlowElement destination)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            _destinations.Add(destination);
            return this;
        }

        public ProcessGatewayBuilder AddIsSynchronizedCondition(Expression<Func<IEnumerable<IConnection>, bool>> isSynchronizedExpression)
        {
            _isSynchronizedExpression = isSynchronizedExpression ?? throw new ArgumentNullException(nameof(isSynchronizedExpression));
            return this;
        }

        public ProcessGatewayBuilder AddEvaluator(Expression<Func<IEnumerable<IConnection>, IEnumerable<IConnection>>> evaluateExpression)
        {
            _evaluateExpression = evaluateExpression ?? throw new ArgumentNullException(nameof(evaluateExpression));
            return this;
        }

        public IProcessGateway Build()
        {
            throw new NotImplementedException();
            //return new ProcessGatewayProxy(_isSynchronizedExpression, _evaluateExpression, _sources.ToList(), _destinations);
        }
    }
}