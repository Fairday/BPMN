namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public abstract class ProcessEvent : FlowElement, IProcessEvent
    {
        public ProcessEvent(string name) : base(name)
        {
            After(Completed)
                .ThenTerminateAt(Finish);
        }

        public ProcessEventLifetime Lifetime { get; protected internal set; }
        public ActivityBoundaryBehavior BoundaryBehavior { get; protected internal set; }
        public ITerminationEvent Finish { get; protected internal set; }
    }
}