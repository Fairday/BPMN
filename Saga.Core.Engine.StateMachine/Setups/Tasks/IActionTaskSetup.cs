using System;

namespace Saga.Core.Engine.StateMachine.Setups.Tasks
{
    public interface IActionTaskSetup : ISetup<IActionTaskSetup>
    {
        IActionTaskSetup SetAction(Action action);
    }
}