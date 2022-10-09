using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Helpers;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class EventBasedExclusiveProcessGateway : FlowElement, IEventBasedExclusiveProcessGateway
    {
        public EventBasedExclusiveProcessGateway(string name, IEnumerable<IFlowElement> sources, IEnumerable<IFlowElement> destinations) : base(name, sources, destinations)
        {
            AsyncSynchronizeLogic = (inputs, token) =>
            {


                return inputs.All(i => i.IsActive);
            };
            AsyncSynchronizeLogic = inputs => inputs;
        }

        public Func<IEnumerable<IInputConnection>, CancellationToken, Task<bool>> AsyncSynchronizeLogic { get; }
        public Func<IEnumerable<IOutputConnection>, CancellationToken, Task<IEnumerable<IOutputConnection>>> AsyncEvaluateLogic { get; }
        public IEnumerable<IProcessEvent> Events { get; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override Task ImpartMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task ActivateAsync(CancellationToken cancellationToken)
        {
            this.MustBe()
                .NotActive()
                .NotCompleted()
                .NotDisabled();

            foreach (var processEvent in Events)
                await processEvent.ActivateAsync(cancellationToken);

            if (await SynchronizeLogic(cancellationToken, Inputs))
            {
                var outputConnections = EvaluateLogic(Outputs);

                IsCompleted = true;
                IsActive = false;

                foreach (var outputConnection in outputConnections)
                    await outputConnection.ActivateAsync(cancellationToken);
            }
        }

        public override Task DisableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Func<IEventBasedExclusiveProcessGateway, CancellationToken, Task<bool>> SynchronizeLogic { get; }
        public Func<IEventBasedExclusiveProcessGateway, IEnumerable<IOutputConnection>> EvaluateLogic { get; }
    }
}