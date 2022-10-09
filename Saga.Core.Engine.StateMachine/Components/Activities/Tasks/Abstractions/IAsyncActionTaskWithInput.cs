using System;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAsyncActionTaskWithInput<TInput> : IProcess<TInput>
    {
        Func<TInput, Task> AsyncAction { get; }
    }
}