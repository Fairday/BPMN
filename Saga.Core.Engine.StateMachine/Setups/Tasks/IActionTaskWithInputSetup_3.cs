using System;

namespace Saga.Core.Engine.StateMachine.Setups.Tasks
{
    public interface IActionTaskWithInputSetup<TInput1, TInput2, TInput3> : ISetup<IActionTaskWithInputSetup<TInput1, TInput2, TInput3>>
    {
        IActionTaskWithInputSetup<TInput1, TInput2, TInput3> SetAction(Action<TInput1, TInput2, TInput3> action);
    }
}