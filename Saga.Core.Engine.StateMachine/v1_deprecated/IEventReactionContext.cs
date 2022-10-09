namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IReactionContext
    {
        void RaiseEvent(IProcessEvent @event);
        void AbortProcess();
        void FinishProcess();
    }

    public interface IReactionContext<out TProcessManager> : IReactionContext
        where TProcessManager : IProcessManager<TProcessManager>
    {
        TProcessManager ProcessManager { get; }
    }

    public interface IEventReactionContext<out TProcessManager> : IReactionContext<TProcessManager>
        where TProcessManager : IProcessManager<TProcessManager>
    {
    }

    //public interface IEventReactionContext<out TProcessManager> : IEventReactionContext<TProcessManager>
    //    where TProcessManager : IProcessManager<TProcessManager>
    //{
    //    void RaiseEvent(IProcessEvent @event);
    //}
}