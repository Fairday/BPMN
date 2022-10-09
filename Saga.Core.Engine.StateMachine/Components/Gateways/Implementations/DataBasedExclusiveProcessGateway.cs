using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class DataBasedExclusiveProcessGateway : FlowElement, IDataBasedExclusiveProcessGateway
    {
        public DataBasedExclusiveProcessGateway(string name, IEnumerable<IFlowElement> sources, IEnumerable<IFlowElement> destinations) : base(name, sources, destinations)
        {
            SynchronizeLogic = async (gateway, token) =>
            {
                var isAnyActive = false;
                var notActiveConnections = new List<IInputConnection>();
                foreach (var inputConnection in gateway.Inputs)
                {
                    if (inputConnection.IsActive)
                    {
                        isAnyActive = true;
                        var outputConnection = gateway.InputOutputDataRouteMap[inputConnection];
                        outputConnection.SetData(inputConnection.GetData());
                        await inputConnection.CompleteAsync(token);
                    }
                    else
                    {
                        notActiveConnections.Add(inputConnection);
                    }
                }

                if (!isAnyActive)
                    return false;

                foreach (var notActiveInputConnection in notActiveConnections)
                    await notActiveInputConnection.DisableAsync(token);

                return true;
            };

            EvaluateLogic = gateway =>
            {
                return gateway.InputOutputDataRouteMap.Where(i => i.Key.IsCompleted).Select(i => i.Value);
            };
        }
        public Func<IDataBasedExclusiveProcessGateway, CancellationToken, Task<bool>> SynchronizeLogic { get; }
        public Func<IDataBasedExclusiveProcessGateway, IEnumerable<IOutputConnection>> EvaluateLogic { get; }
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
            if (await SynchronizeLogic(this, cancellationToken))
            {
                var outputConnections = EvaluateLogic(this);

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