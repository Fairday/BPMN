using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IActionTaskWithInput<TInput> : IProcess<TInput>
    {
        Action<TInput> Action { get; }
    }

    //public interface IActionTaskWithInput<TInput1, TInput2> : ITask, IAcceptData<TInput1, TInput2>, IHandleError
    //{
    //    Action<TInput1, TInput2> Action { get; }
    //}

    //public interface IActionTaskWithInput<TInput1, TInput2, TInput3> : ITask, IAcceptData<TInput1, TInput2, TInput3>, IHandleError
    //{
    //    Action<TInput1, TInput2, TInput3> Action { get; }
    //}

    //public interface IActionTaskWithInput<TInput1, TInput2, TInput3, TInput4> : ITask, IAcceptData<TInput1, TInput2, TInput3, TInput4>, IHandleError
    //{
    //    Action<TInput1, TInput2, TInput3, TInput4> Action { get; }
    //}
}