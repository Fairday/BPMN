using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IEventBasedExclusiveProcessGateway : IProcessGateway<IEventBasedExclusiveProcessGateway>
    {
        IEnumerable<IProcessEvent> Events { get; }
    }
}