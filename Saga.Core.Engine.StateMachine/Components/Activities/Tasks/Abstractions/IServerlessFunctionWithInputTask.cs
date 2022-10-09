using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components.Tasks.Abstractions
{
    public interface IServerlessFunctionWithInputTask<TInput> : IProcess<TInput>
    {
    }
}