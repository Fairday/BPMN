namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public enum ActivityExecutionStatus
    {
        NotStarted = 0,
        Running = 10,
        Canceled = 20,
        Completed = 30,
        Faulted = 40
    }
}