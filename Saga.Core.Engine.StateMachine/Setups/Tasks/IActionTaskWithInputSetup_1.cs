using System;

namespace Saga.Core.Engine.StateMachine.Setups.Tasks
{
    public interface IActionTaskWithInputSetup<TInput1> : ISetup<IActionTaskWithInputSetup<TInput1>>
    {
        IActionTaskWithInputSetup<TInput1> SetAction(Action<TInput1> action);
    }
}