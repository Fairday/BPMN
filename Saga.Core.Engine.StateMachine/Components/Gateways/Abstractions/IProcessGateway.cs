using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessGateway<in TProcessGateway> : IMultiConnectionProcessUnit
        where TProcessGateway : IProcessGateway<TProcessGateway>
    {
        Func<TProcessGateway, CancellationToken, Task<bool>> SynchronizeLogic { get; }
        Func<TProcessGateway, IEnumerable<IOutputConnection>> EvaluateLogic { get; }
    }
}