using System;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.WorkflowBuilders.Abstractions
{
    public interface ISpecificProcessUnitTypeWorkflowBuilder<TProcessUnit> : IWorkflowBuilder
        where TProcessUnit : IFlowElement
    {
        IWorkflowBuilder<TExtractingData> ExtractData<TExtractingData>(Func<TProcessUnit, TExtractingData> dataExtractor);
    }
}