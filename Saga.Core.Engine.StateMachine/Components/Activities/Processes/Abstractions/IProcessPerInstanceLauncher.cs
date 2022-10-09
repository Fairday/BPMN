using System;
using System.Collections.Generic;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components.Tasks.Abstractions
{
    public interface IProcessPerInstanceLauncher<TProcess, TInstance> : IProcess<TInstance>
        where TInstance : class, IEquatable<TInstance>
        where TProcess : IProcess<TInstance>
    {
        IEnumerable<TProcess> InstanceProcesses { get; }

        //start condition, when?
    }
}