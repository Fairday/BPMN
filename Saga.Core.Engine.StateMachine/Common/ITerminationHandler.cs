using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ITerminationHandler
    {
        Task HandleFinishAsync(CancellationToken cancellationToken);
        Task HandleErrorAsync(Exception errorObject, CancellationToken cancellationToken);
        Task HandleErrorAsync<TError>(TError errorObject, CancellationToken cancellationToken)
            where TError : Exception;
    }

    public interface ITerminationHandler<TResult> : ITerminationHandler
    {
        Task HandleFinishWithResultAsync(TResult result, CancellationToken cancellationToken);
    }
}