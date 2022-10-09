using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IActionTask : IProcess<Nothing>
    {
        Action Action { get; }
    }
}