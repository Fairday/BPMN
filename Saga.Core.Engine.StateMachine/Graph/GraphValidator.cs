using System;
using System.Collections.Generic;
using System.Linq;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class GraphValidator : IGraphValidator
    {
        private readonly IGraphValidationRule[] _rules;

        public GraphValidator(IEnumerable<IGraphValidationRule> rules)
        {
            _rules = rules?.ToArray() ?? throw new ArgumentNullException(nameof(rules));
        }

        public bool Validate(IGraph graph)
        {
            if (graph == null) 
                throw new ArgumentNullException(nameof(graph));

            return _rules.All(r => r.Check(graph));
        }
    }
}