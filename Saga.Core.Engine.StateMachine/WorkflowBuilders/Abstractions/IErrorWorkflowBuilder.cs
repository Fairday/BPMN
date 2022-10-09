using System;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.WorkflowBuilders.Abstractions
{
    public interface IErrorWorkflowBuilder
    {
        IWorkflowBuilder Catch(IAcceptData<Exception> task);
        IWorkflowBuilder Catch<TException>(IAcceptData<TException> task)
            where TException : Exception;

        IWorkflowBuilder<TResult> Catch<TException, TResult, TAcceptAndTransmitDataUnit>(TAcceptAndTransmitDataUnit processUnit)
            where TAcceptAndTransmitDataUnit : IAcceptData<TException>, ITransmitData<TResult>
            where TException : Exception;
        IWorkflowBuilder<TResult> Catch<TResult, TAcceptAndTransmitDataUnit>(TAcceptAndTransmitDataUnit processUnit)
            where TAcceptAndTransmitDataUnit : IAcceptData<Exception>, ITransmitData<TResult>;

        void ThenTerminateAt(ITerminationEvent terminationEvent);
    }
}