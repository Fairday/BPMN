using System.Collections.Generic;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine
{
    public interface ITransmitData<TOutgoingData>
    {
        IEnumerable<IAcceptData<TOutgoingData>> DataAcceptors { get; }
    }
}