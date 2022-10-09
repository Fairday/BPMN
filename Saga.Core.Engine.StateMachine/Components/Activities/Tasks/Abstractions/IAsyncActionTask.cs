using System;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAsyncActionTask : IProcess<Nothing>
    {
        Func<Task> AsyncAction { get; }
    }
}