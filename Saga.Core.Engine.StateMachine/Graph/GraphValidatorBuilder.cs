using System;
using System.Collections.Generic;
using System.Linq;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class GraphValidatorBuilder : IGraphValidatorBuilder
    {
        private bool _validatorBuilt;
        private readonly HashSet<Type> _rules;

        public GraphValidatorBuilder()
        {
            _rules = new HashSet<Type>();
        }

        public IGraphValidatorBuilder Use<TRule>() 
            where TRule : IGraphValidationRule, new()
        {
            if (_validatorBuilt)
                throw new InvalidOperationException("The validator have been already built");

            var ruleType = typeof(TRule);

            if (ruleType.IsAbstract)
                throw new ArgumentException(ruleType.Name + " must be non abstract class");

            _rules.Add(ruleType);
            return this;
        }

        public IGraphValidator Build()
        {
            _validatorBuilt = true;
            return new GraphValidator(_rules.Select(ruleType => (IGraphValidationRule)Activator.CreateInstance(ruleType)));
        }
    }
}