using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ITaskWithCompensation : IProcess<Nothing>, ICompensationActivity
    {
        Action CompensationAction { get; }
    }
}