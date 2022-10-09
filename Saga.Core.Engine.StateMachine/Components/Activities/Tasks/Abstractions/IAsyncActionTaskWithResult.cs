using System;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAsyncActionTaskWithResult<TResult> : IProcessWithResult<Nothing, TResult>
    {
        Func<Task<TResult>> Action { get; }
    }
}