using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.WorkflowBuilders.Abstractions
{
    public interface IWorkflowBuilder
    {
        ISpecificProcessUnitTypeWorkflowBuilder<TProcessUnit> ThenClarifyType<TProcessUnit>(TProcessUnit processUnit)
            where TProcessUnit : IFlowElement;
        //TODO: throw if IProcessUnit implements IAcceptData
        IWorkflowBuilder Then(IFlowElement flowElement);
        void ThenParallel(params IFlowElement[] processUnits);
        void ThenTerminateAt(ITerminationEvent terminationEvent);
    }

    public interface IWorkflowBuilder<TInput>
    {
        ISpecificProcessUnitTypeWorkflowBuilder<TProcessUnit> TransmitTo<TProcessUnit>(TProcessUnit processUnit)
            where TProcessUnit : IFlowElement, IAcceptData<TInput>;
        void TransmitToTerminatation(ITerminationEvent<TInput> terminationEvent);
    }
}