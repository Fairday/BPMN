using System;

namespace Saga.Core.Engine.StateMachine.Setups.Tasks
{
    public interface IActionTaskWithInputSetup<TInput1, TInput2, TInput3, TInput4> : ISetup<IActionTaskWithInputSetup<TInput1, TInput2, TInput3, TInput4>>
    {
        IActionTaskWithInputSetup<TInput1, TInput2, TInput3, TInput4> SetAction(Action<TInput1, TInput2, TInput3, TInput4> action);
    }
}