using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ITransactionalProcess : IProcess<Nothing>
    {
        IEnumerable<ICompensationActivity> Activities { get; }
    }
}