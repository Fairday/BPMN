using System;
using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IMultiInstanceTask<out TInstance> : IProcess<Nothing>
    {
        ActivityExecutionBehavior ExecutionBehavior { get; }
        Func<IEnumerable<TInstance>> InstancesFactory { get; }
    }
}