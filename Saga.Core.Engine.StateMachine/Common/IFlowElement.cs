using System;
using System.Threading;
using System.Threading.Tasks;
using OrchestratoR.Core.Common;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IFlowElement : IHaveName, IVisitable, IEquatable<IFlowElement>
    {
        bool IsActive { get; }
        bool IsDisabled { get; }
        bool IsCompleted { get; }
        Task ActivateAsync(CancellationToken cancellationToken);
        Task DisableAsync(CancellationToken cancellationToken);
    }
}