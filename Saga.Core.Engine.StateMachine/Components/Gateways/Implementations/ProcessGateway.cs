using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Exceptions;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ProcessGateway : FlowElement, IProcessGateway
    {
        public ProcessGateway(
            string name,
            Expression<Func<IEnumerable<IConnection>, bool>> isSynchronizedExpression, 
            Expression<Func<IEnumerable<IConnection>, IEnumerable<IConnection>>> evaluateExpression,
            IEnumerable<IFlowElement> sources,
            IEnumerable<IFlowElement> destinations) : base(name, sources, destinations)
        {
            IsSynchronizedExpression = isSynchronizedExpression ?? throw new ArgumentNullException(nameof(isSynchronizedExpression));
            EvaluateExpression = evaluateExpression ?? throw new ArgumentNullException(nameof(evaluateExpression));
        }

        public Expression<Func<IEnumerable<IConnection>, bool>> IsSynchronizedExpression { get; }
        public Expression<Func<IEnumerable<IConnection>, IEnumerable<IConnection>>> EvaluateExpression { get; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var isSynchronized = IsSynchronizedExpression.Compile()(InputConnections);
            if (isSynchronized)
            {
                var activateCandidates = EvaluateExpression.Compile()(OutputConnections);
                if (activateCandidates == null)
                    ProcessManagerException.Throw();

                // ReSharper disable once AssignNullToNotNullAttribute
                var activateCandidatesMap = new HashSet<IConnection>(activateCandidates);

                foreach (var outputConnection in OutputConnections)
                {
                    if (!activateCandidatesMap.Contains(outputConnection))
                        await outputConnection.DisableAsync(cancellationToken);
                }

                IsActive = false;

                // ReSharper disable once PossibleNullReferenceException
                foreach (var outputConnection in activateCandidatesMap)
                    await outputConnection.ActivateAsync(cancellationToken);
            }
        }

        public override async Task ActivateAsync(CancellationToken cancellationToken)
        {
            if (!IsActive)
                IsActive = true;

            await ExecuteAsync(cancellationToken);
        }

        public override async Task DisableAsync(CancellationToken cancellationToken)
        {
            if (!IsDisabled)
            {
                IsDisabled = true;
                foreach (var outputConnection in OutputConnections)
                    await outputConnection.DisableAsync(cancellationToken);
            }
        }
    }
}