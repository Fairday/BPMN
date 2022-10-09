using System;
using System.Threading;
using System.Threading.Tasks;
using OrchestratoR.Core.Helpers;
using Saga.Core.Engine.StateMachine.WorkflowBuilders.Abstractions;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public abstract class FlowElement : IFlowElement
    {
        public FlowElement(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            EventListener.SubscribeToSpecificEventFromSource<IFinishedEvent>(Activated, (e) =>
            {
                IsActive = true;
            });

            EventListener.SubscribeToSpecificEventFromSource<IFinishedEvent>(Disabled, (e) =>
            {
                IsDisabled = true;
            });

            EventListener.SubscribeToSpecificEventFromSource<IFinishedEvent>(Completed, (e) =>
            {
                IsActive = false;
                IsCompleted = true;
            });
        }

        public string Name { get; }
        public bool IsActive { get; internal set; }
        public bool IsDisabled { get; internal set; }
        public bool IsCompleted { get; internal set; }
        protected IEmptyEvent Completed { get; }
        protected IEmptyEvent Activated { get; }
        protected IEmptyEvent Disabled { get; }
        protected IProcessEventListener EventListener { get; }
        protected IProcessEventRiser EventRiser { get; }

        public abstract Task Accept(IProcessVisitor visitor);
        public abstract Task ActivateAsync(CancellationToken cancellationToken);
        public abstract Task DisableAsync(CancellationToken cancellationToken);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is IFlowElement other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(IFlowElement other)
        {
            return other != null && ProcessUnitStringComparer.Equals(Name, other.Name);
        }

        protected ISpecificProcessUnitTypeWorkflowBuilder<TStartEvent> StartAt<TStartEvent>(TStartEvent startEvent)
            where TStartEvent : IFlowElement, IStartEvent
        {
            throw new NotImplementedException();
        }

        protected IWorkflowBuilder AfterAll(params IFlowElement[] processUnits)
        {
            throw new NotImplementedException();
        }

        protected IWorkflowBuilder<TResult> AfterResult<TResult>(IActivity<TResult> processUnit)
        {
            throw new NotImplementedException();
        }

        protected IWorkflowBuilder After(IFlowElement flowElement)
        {
            throw new NotImplementedException();
        }

        protected IErrorWorkflowBuilder AfterError(IErrorEvent errorEvent)
        {
            throw new NotImplementedException();
        }
    }
}