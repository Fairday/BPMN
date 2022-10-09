using System;

namespace Saga.Core.Engine.StateMachine.Setups.Tasks
{
    public interface IActionTaskWithInputSetup<TInput1, TInput2> : ISetup<IActionTaskWithInputSetup<TInput1, TInput2>>
    {
        IActionTaskWithInputSetup<TInput1, TInput2> SetAction(Action<TInput1, TInput2> action);
    }
}