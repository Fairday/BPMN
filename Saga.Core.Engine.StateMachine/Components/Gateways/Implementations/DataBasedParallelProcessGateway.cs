using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class DataBasedParallelProcessGateway<TData1, TData2> : FlowElement, IDataBasedParallelProcessGateway
    {
        public DataBasedParallelProcessGateway(string name, IEnumerable<IFlowElement> sources, IEnumerable<IFlowElement> destinations) : base(name, sources, destinations)
        {
            SynchronizeLogic = inputs =>
            {
                var areAllActive = inputs.All(i => i.IsActive);

                if (!areAllActive) 
                    return false;

                foreach (var inputConnection in inputs)
                {
                    var outputConnection = InputOutputDataRouteMap[inputConnection];
                    outputConnection.SetData(inputConnection.GetData());
                }

                return true;
            };

            EvaluateLogic = outputs => outputs;
        }

        public Func<IEnumerable<IInputConnection>, CancellationToken, Task<bool>> AsyncSynchronizeLogic { get; }
        public Func<IEnumerable<IOutputConnection>, CancellationToken, Task<IEnumerable<IOutputConnection>>> AsyncEvaluateLogic { get; }
        public Dictionary<IInputConnection, IOutputConnection> InputOutputDataRouteMap { get; }

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
            if (SynchronizeLogic(Inputs))
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
    }
}